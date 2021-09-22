using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Kasa.Config;
using Soneta.Magazyny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class DokumentyService
    {
        public List<DamDokument> GetDokumenty(string typDokumentu)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamDokument> dokumenty = new List<DamDokument>();

                //DL dokumenty
                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                // Definicja dla której szukamy
                string _typDokumentu = "ZK";
                Soneta.Handel.DefDokHandlowego def = hamodule.DefDokHandlowych.WgSymbolu[_typDokumentu];
                // Magazyn dla którego szukamy
                //Soneta.Magazyny.Magazyn mag = hamodule.Magazyny.Magazyny.WgSymbol["F"];
                //Mając powyższe możemy utworzyć View z założonym odpowiednim warunkiem:

                // Przykład #2
                Soneta.Business.View view1 = hamodule.DokHandlowe.CreateView();
                // i zakładamy warunki:
                //view1.Condition = new FieldCondition.Equal("Definicja", def);
                //view1.Condition = new FieldCondition.Equal(Magazyn, mag);
                //view1.Condition = new FieldCondition.Equal(Stan, Soneta.Handel.StanDokumentuHandlowego.Bufor);
                foreach (DokumentHandlowy dok in view1)
                {
                    DamDokument damDokument = new DamDokument();
                    damDokument.PozycjeDokumentu = new List<DamDokumentPozycja>();

                    damDokument.ID = dok.ID;
                    damDokument.Numer = dok.Numer.NumerPelny;
                    damDokument.Typ = dok.Definicja.Symbol;

                    damDokument.Data = dok.Data;
                    damDokument.DataOperacji = dok.DataOperacji;
                    if(dok.Kontrahent != null)
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.BruttoCy.Value;
                    
                    
                    //DL pozycje dokumentu
                    //dok.Pozycje; 
                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamDokumentPozycja pozycja = new DamDokumentPozycja();
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.WartoscCy.Value;
                        pozycja.StawkaVAT = poz.DefinicjaStawki.Kod;
                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    //foreach (Zasob zasob in dok.ZasobyWszystkie)
                    //{
                    //    DamDokumentZasob damZasob = new DamDokumentZasob();
                    //    damZasob.Kod = zasob.Towar.Kod;
                    //    damZasob.Typ = zasob.Partia.Typ.ToString();
                    //    damZasob.Ilosc = zasob.IlośćZasobu.Value;
                    //    damZasob.Wartosc = zasob.Partia.Wartosc;
                    //    damZasob.Cena = zasob.Partia.Cena;
                    //    damZasob.Dokument = zasob.Partia.Dokument.Numer.NumerPelny;
                    //    damZasob.DokumentPartiiPierwotnej = zasob.PartiaPierwotna.Dokument.Numer.NumerPelny;

                    //    damDokument.ZasobyDokumentu.Add(damZasob);
                    //}
                   
                    //foreach (DokumentHandlowy dokPow in dok.Nadrzędne)
                    //{
                    //    DamDokumentPowiazany dokumentPowiazany = new DamDokumentPowiazany();
                    //    dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                    //    dokumentPowiazany.Data = dokPow.Data;
                    //    if (dokumentPowiazany.Kontrahent != null)
                    //        dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                    //    dokumentPowiazany.Netto = dokPow.Suma.Netto;
                    //    dokumentPowiazany.VAT = dokPow.Suma.VAT;
                    //    dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                    //    damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    //}

                    //foreach (DokumentHandlowy dokPow in dok.Podrzędne)
                    //{
                    //    DamDokumentPowiazany dokumentPowiazany = new DamDokumentPowiazany();
                    //    dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                    //    dokumentPowiazany.Data = dokPow.Data;
                    //    if (dokumentPowiazany.Kontrahent != null)
                    //        dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                    //    dokumentPowiazany.Netto = dokPow.Suma.Netto;
                    //    dokumentPowiazany.VAT = dokPow.Suma.VAT;
                    //    dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                    //    damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    //}
                    dokumenty.Add(damDokument);
                }
                return dokumenty;
            }
        }
    }
}