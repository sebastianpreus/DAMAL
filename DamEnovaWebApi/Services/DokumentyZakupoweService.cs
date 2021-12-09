using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Magazyny;
using System;
using System.Collections.Generic;

namespace DamEnovaWebApi.Services
{
    public class DokumentyZakupoweService
    {
        public List<DamDokumentZakupowy> GetDokumenty(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                //List<DamDokumentZakupowy> dokumenty = new List<DamDokumentZakupowy>();

                ////DL dokumenty
                //Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                //// Definicja dla której szukamy
                ////string _typDokumentu = "ZK";
                ////DefDokHandlowego def = hamodule.DefDokHandlowych.WgSymbolu[_typDokumentu];
                //// Magazyn dla którego szukamy
                ////Soneta.Magazyny.Magazyn mag = hamodule.Magazyny.Magazyny.WgSymbol["F"];
                ////Mając powyższe możemy utworzyć View z założonym odpowiednim warunkiem:

                //// Przykład #2

                //DateTime start = DateTime.Now;
                //int count = 0;

                ////RowCondition rc = new FieldCondition.GreaterEqual("Data", "2021-01-01")
                ////                & new FieldCondition.LessEqual("Data", "2021-10-31")
                ////                & new FieldCondition.Equal("Kategoria", "Zakup");


                ////SubTable view1 = hamodule.DokHandlowe.WgDaty[rc];

                //// i zakładamy warunki:
                ////view1.Condition = new FieldCondition.Equal("Definicja", def);
                ////view1.Condition = new FieldCondition.Equal(Magazyn, mag);
                ////view1.Condition = new FieldCondition.Equal(Stan, Soneta.Handel.StanDokumentuHandlowego.Bufor);


                //View view1 = hamodule.DokHandlowe.CreateView();

                //filter.FilterView(view1);
                //view1.Condition &= new FieldCondition.Equal("Kategoria", "Zakup");


                //////////////////////////////////////////
                ///

                DateTime start = DateTime.Now;
                int count = 0;
                List<DamDokumentZakupowy> dokumenty = new List<DamDokumentZakupowy>();

                //DL dokumenty
                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                // Definicja dla której szukamy
                string _typDokumentu = "ZK";
                DefDokHandlowego def = hamodule.DefDokHandlowych.WgSymbolu[_typDokumentu];
                // Magazyn dla którego szukamy
                //Soneta.Magazyny.Magazyn mag = hamodule.Magazyny.Magazyny.WgSymbol["F"];
                //Mając powyższe możemy utworzyć View z założonym odpowiednim warunkiem:

                // Przykład #2
                View view1 = hamodule.DokHandlowe.CreateView();

                // i zakładamy warunki:
                //view1.Condition = new FieldCondition.Equal("Definicja", def);
                //view1.Condition = new FieldCondition.Equal(Magazyn, mag);
                //view1.Condition = new FieldCondition.Equal(Stan, Soneta.Handel.StanDokumentuHandlowego.Bufor);

                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Zakup");
                //view1.Condition &= new FieldCondition.GreaterEqual("ID", 291914);


                //SubTable sub = new SubTable(hamodule.DokHandlowe.WgData, DateTime.Now);

                //sub.get


                foreach (DokumentHandlowy dok in view1)
                {
                    count += 1;
                    DamDokumentZakupowy damDokument = new DamDokumentZakupowy();
                    damDokument.PozycjeDokumentu = new List<DamDokumentZakupowyPozycja>();
                    damDokument.ZasobyDokumentu = new List<DamDokumentZakupowyZasob>();
                    damDokument.DokumentyPowiazane = new List<DamDokumentZakupowyPowiazany>();

                    damDokument.ID = dok.ID;
                    damDokument.Numer = dok.Numer.NumerPelny;
                    damDokument.Typ = dok.Definicja.Symbol;


                    damDokument.Zatwierdzony = dok.Zatwierdzony;
                    damDokument.Korekta = dok.Korekta;
                    damDokument.Data = dok.Data;
                    damDokument.DataOperacji = dok.DataOperacji;
                    if(dok.Kontrahent != null)
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.BruttoCy.Value;
                    damDokument.Waluta = dok.Suma.BruttoCy.Symbol;
                    
                    
                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamDokumentZakupowyPozycja pozycja = new DamDokumentZakupowyPozycja();
                        //PK
                        pozycja.ID = poz.ID;
                        pozycja.DamDokumentId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.WartoscCy.Value;
                        pozycja.StawkaVAT = poz.DefinicjaStawki.Kod;
                        pozycja.Waluta = poz.Suma.BruttoCy.Symbol;

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamDokumentZakupowyZasob damZasob = new DamDokumentZakupowyZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamDokumentId = dok.ID;

                        damZasob.Kod = zasob.Towar.Kod;
                        damZasob.Typ = zasob.Partia.Typ.ToString();
                        damZasob.Ilosc = zasob.IlośćZasobu.Value;
                        damZasob.JednostkaMiary = zasob.IlośćZasobu.Symbol;
                        damZasob.Wartosc = zasob.Partia.Wartosc;
                        damZasob.Cena = zasob.Partia.Cena;
                        damZasob.Dokument = zasob.Partia.Dokument.Numer.NumerPelny;
                        damZasob.DokumentPartiiPierwotnej = zasob.PartiaPierwotna.Dokument.Numer.NumerPelny;

                        damDokument.ZasobyDokumentu.Add(damZasob);
                    }

                    foreach (DokumentHandlowy dokPow in dok.Nadrzędne)
                    {
                        DamDokumentZakupowyPowiazany dokumentPowiazany = new DamDokumentZakupowyPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamDokumentId = dok.ID;

                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Data = dokPow.Data;
                        if (dokumentPowiazany.Kontrahent != null)
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                        dokumentPowiazany.Netto = dokPow.Suma.Netto;
                        dokumentPowiazany.VAT = dokPow.Suma.VAT;
                        dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                        dokumentPowiazany.Waluta = dokPow.Suma.BruttoCy.Symbol;

                        damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    }

                    foreach (DokumentHandlowy dokPow in dok.Podrzędne)
                    {
                        DamDokumentZakupowyPowiazany dokumentPowiazany = new DamDokumentZakupowyPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamDokumentId = dok.ID;

                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Data = dokPow.Data;
                        if (dokumentPowiazany.Kontrahent != null)
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                        dokumentPowiazany.Netto = dokPow.Suma.Netto;
                        dokumentPowiazany.VAT = dokPow.Suma.VAT;
                        dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                        dokumentPowiazany.Waluta = dokPow.Suma.BruttoCy.Symbol;

                        damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    }
                    dokumenty.Add(damDokument);
                }

                var ttttttt = DateTime.Now - start;
                var ilosc = count;
                return dokumenty;
            }
        }
    }
}