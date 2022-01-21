using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class TowaryService
    {
        internal List<DamTowar> GetTowary(Filter filter)
        {
            throw new NotImplementedException();
        }

        internal void PostTowar(DamTowar damTowar)
        {

            ////   TEST DODAWANIA DOKUMENTU ///////////////
            //CreateDocumentPZ();
            ///////////////////////////////////////////////
            

            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);
                using (ITransaction trans = session.Logout(true))
                {
                    Towar towar = (Towar)tm.Towary.WgEAN[damTowar.EAN].GetNext();
                    if (towar == null)
                    {
                        Towar newTowar = new Towar();

                        tm.Towary.AddRow(newTowar);

                        newTowar.Kod = damTowar.Kod;
                        newTowar.Nazwa = damTowar.Nazwa;
                        newTowar.EAN = damTowar.EAN;

                    }
                    trans.Commit();
                }
                session.Save(); 
            }
        }



        private void CreateDocumentPZ()
        {
            //////////////////////////////////////////////////////////////////
            // Rozpoczęcie tworzenia dokumentu (w ogóle operacji na logice
            // biznesowej) polega na utworzeniu obiektu sesji (Session),
            // w którym będą odbywać się poszczególne operacje.
            // Pierwszy parametr określa, czy sesja jest tylko do odczytu
            // danych, drugi parametr, czy sesja będzie modyfikować ustawienia
            // konfiguracyjne (tj definicje dokumentów, jednostki, 
            // definicje cen, itp). Standardowo obydwa parametry dajemy false.
            //
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {

                //////////////////////////////////////////////////////////////////
                // Po utworzeniu sesji dobrze jest sobie przygotować odpowiednie 
                // zmiene reprezentujące poszczególne moduły programu w tej sesji.
                // Wystarczy przygotwać tylko te moduły, które będą nam potrzebne.
                //
                HandelModule hm = HandelModule.GetInstance(session);
                TowaryModule tm = TowaryModule.GetInstance(session);
                MagazynyModule mm = MagazynyModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);

                //////////////////////////////////////////////////////////////////
                // Wszystkie operacje wykonujemy w transakcji sesji którą należy
                // na początku otworzyć. W transakcji możemy wskazać czy będą 
                // robione zmiany w danych.
                //
                using (ITransaction trans = session.Logout(true))
                {

                    //////////////////////////////////////////////////////////////////
                    // Następnie należy utworzyć nowy obiekt reprezentujący dokument
                    // handlowy (nagłówek dokumentu). 
                    //
                    DokumentHandlowy dokument = new DokumentHandlowy();

                    //////////////////////////////////////////////////////////////////
                    // Nowy dokument nalezy również związać z definicją dokumentu 
                    // handlowego. W tym przypadku wyszukujemy definicje wyszukujemy
                    // wg jej symbolu "PZ".
                    //
                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu["PZ 2"];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu PZ.");
                    dokument.Definicja = definicja;

                    /////////////////////////////////////////////////////////////////
                    // Dokument należy też przypisać do magazynu, do którego będzie
                    // przyjmowany towar. Poniżej przypisywany jest standardowy
                    // magazyn programu "Firma".
                    //
                    dokument.Magazyn = mm.Magazyny.Firma;

                    /////////////////////////////////////////////////////////////////
                    // Oraz dodajemy nowo utworzony dokument do aktualnej sesji.
                    //
                    hm.DokHandlowe.AddRow(dokument);

                    /////////////////////////////////////////////////////////////////
                    // Przyjęcie magazynowe PZ (z zewnątrz) wymaga również
                    // przypisania kontrahenta, od którego towaru jest przyjmowany.
                    // Przykład prezentuje przypisanie dokumentowi kontrahenta
                    // o kodzie "ABC".
                    //Kontrahent kontrahent = cm.Kontrahenci.WgKodu["ABC"];
                    Kontrahent kontrahent = cm.Kontrahenci.WgKodu["FLEETCOR SK"];
                    if (kontrahent == null)
                        throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie ABC.");
                    dokument.Kontrahent = kontrahent;

                    /////////////////////////////////////////////////////////////////
                    // PUNKT A ******************************************************
                    // W kartotece towarów wyszukać towar. Przykład poniżej
                    // prezentuje wyszukanie towaru wg kodu EAN "2000000000022". 
                    // Ponieważ w kartotece może znajdować się wiele towarów o tym 
                    // samym kodzie wybrazy zostanie pierwszy z nich.
                    //
                    //Towar towar = (Towar) tm.Towary.WgEAN["2000000000954"].GetNext();
                    //Towar towar = (Towar)tm.Towary.WgKodu["R412010564"];//.GetNext();
                    Towar towar = (Towar)tm.Towary.WgEAN["EANTT7"].GetNext();
                    if (towar != null)
                    {
                        //////////////////////////////////////////////////////////////
                        // Utworzyć nowy obiekt pozycji dokumentu handlowego, który
                        // zostanie dodany do sescji.
                        //
                        PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                        hm.PozycjeDokHan.AddRow(pozycja);

                        //////////////////////////////////////////////////////////////
                        // Przypisać towar do nowo utworzonej pozycji dokumentu, czyli
                        // wskazać, który towar ma być przyjęty do magazynu.
                        //
                        pozycja.Towar = towar;  //Błąd kompilacji algorytmów cen.

                        //////////////////////////////////////////////////////////////
                        // W pozycji dokumentu należy jeszcze wprowadzić ilość
                        // towaru przyjmowanego na magazyn. Ilość reprezentowana jest
                        // przez liczbę 10 będącą wartością ilości (pierwszy parametr) 
                        // oraz jednostkę opisującą tę ilość (drugi parametr). Jeżeli
                        // jednostka jest null, to przyjmowana jest jednostka z
                        // karty towarowej.
                        // Poniżej znajduje się również wykomentowany przykład, w
                        // którym w sposób jawny jest wskazanie na jednostkę w metrach.
                        //
                        pozycja.Ilosc = new Quantity(10, null);
                        // pozycja.Ilosc = new Quantity(10, "m");

                        //////////////////////////////////////////////////////////////
                        // Pozycji dokumentu należy również przypisać cenę w jakiej
                        // będzie on wprowadzany do magazynu. (cena zakupu)
                        // Poniżej przypisywana jest cena w PLN. Dlatego nie jest
                        // wyspecyfikowany drugi parametr określający walutę ceny.
                        //
                        pozycja.Cena = new DoubleCy(12.34);

                        //////////////////////////////////////////////////////////////
                        // Poszczególnym pozycjom można przypisać również dodatkowe
                        // cechy, które zależne są od konfiguracji programu. Przykład
                        // pokazuje jak ustawić cechę z numerem beli.
                        // Kod jest wykomentowany, ponieważ baza demo nie posiada
                        // zdefiniowanej tej cechy.
                        //
                        //pozycja.Features["Numer beli"] = "123456";
                    }

                    /////////////////////////////////////////////////////////////////
                    // Jeżeli na dokument ma zawierać więcej pozycji magazynowych
                    // to należy ponownie przejść do PUNKTU A.
                    //

                    /////////////////////////////////////////////////////////////////
                    // Dokumentowi podobnie jak pozycji dokumentu również można
                    // przypisać dodatkowe cechy zależne od konfiguracji programu. 
                    // Przykład pokazuje jak ustawić cechę z lokalizacją.
                    // Kod jest wykomentowany, ponieważ baza demo nie posiada
                    // zdefiniowanej tej cechy.
                    //
                    //dokument.Features["Lokalizacja"] = "AB/12";

                    /////////////////////////////////////////////////////////////////
                    // Po dokonaniu wszystkich operacji na dokumencie można ten
                    // dokument wprowadzić (zatwierdzić), co powoduje zabezpieczenie 
                    // przed przypadkową edycją tego dokumentu oraz przeniesienie go
                    // do ewidencji dokumentów księgowych.
                    //
                    dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;

                    /////////////////////////////////////////////////////////////////
                    // Wszystkie operacje zostały poprawnie zakończone i zapewne 
                    // chcemy zatwierdzić transakcję sesji.
                    //
                    trans.Commit();
                }

                ////////////////////////////////////////////////////////////////////
                // Powyższe operacje były wykonywane na sesji, czyli w pamięci.
                // Teraz należy rezultat prac zapisać do bazy danych.
                //
                session.Save();
            }

            ////////////////////////////////////////////////////////////////////
            // I to wszystko. Dokument PZ znajduje się w bazie.
            //
        }
    }
}