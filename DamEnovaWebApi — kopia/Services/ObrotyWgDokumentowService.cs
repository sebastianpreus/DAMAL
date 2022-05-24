using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Magazyny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class ObrotyWgDokumentowService
    {
        public List<DamObrotyWgDokumentow> GetObroty(string kierunekMagazynu, Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamObrotyWgDokumentow> obroty = new List<DamObrotyWgDokumentow>();

                HandelModule hamodule = HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();

                //view1.Condition = new FieldCondition.Equal("Kategoria", "Zamówienie odbiorcy");
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("KierunekMagazynu", kierunekMagazynu);
                view1.Condition &= new FieldCondition.In("TypPartii", "Magazynowy", "Zarezerwowany", "Zamówiony");

                // obroty wg dokumentow
                ObrotyDokumentuWorker o = new ObrotyDokumentuWorker();

                foreach (DokumentHandlowy dok in view1)
                {
                    DamObrotyWgDokumentow damDokument = new DamObrotyWgDokumentow();
                    damDokument.PozycjeDokumentu = new List<DamObrotyWgDokumentowPozycja>();
                    damDokument.DokumentyPowiazane = new List<DamObrotyWgDokumentowPowiazany>();
                    damDokument.ZasobyDokumentu = new List<DamObrotyWgDokumentowZasob>();

                    o.Dokument = dok;
                    
                    damDokument.ID = dok.ID;

                    damDokument.Typ = dok.Definicja.Symbol;
                    damDokument.Zatwierdzony = dok.Zatwierdzony;
                    damDokument.Korekta = dok.Korekta;
                    damDokument.Data = dok.Data;
                    damDokument.Numer = dok.Numer.NumerPelny;
                    if (dok.Kontrahent != null)
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Marza = o.Marża;
                    damDokument.MarzaProcent = o.MarżaProcent.ToString();
                    damDokument.Netto = dok.SumaPozycjiTowProd.Netto;
                    damDokument.WartoscR = dok.SumaBezDup.Netto;
                    damDokument.WartoscP = o.WartośćZakupu;
                    damDokument.WartoscRstanUjemny = o.DoRozliczania;
                    damDokument.Waluta = dok.Suma.BruttoCy.Symbol;


                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamObrotyWgDokumentowPozycja pozycja = new DamObrotyWgDokumentowPozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamObrotyWgDokumentowId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.Wartość;
                        pozycja.Waluta = poz.Suma.BruttoCy.Symbol;

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamObrotyWgDokumentowZasob damZasob = new DamObrotyWgDokumentowZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamObrotyWgDokumentowId = dok.ID;

                        damZasob.OkresMagazynowy = zasob.Okres.ToStringValue();
                        damZasob.Towar = zasob.Towar.ToStringValue();
                        damZasob.Typ = zasob.Partia.Typ.ToString();
                        damZasob.IloscZasobu = zasob.Ilosc.Value;
                        damZasob.JednostkaMiary = zasob.IlośćZasobu.Symbol;
                        damZasob.Wartosc = zasob.Partia.Wartosc;
                        damZasob.Cena = zasob.Partia.Cena;
                        damZasob.DokumentPartia = zasob.Partia.Dokument.Numer.NumerPelny;
                        damZasob.DokumentPartiaPierwotna = zasob.PartiaPierwotna.Dokument.Numer.NumerPelny;

                        damDokument.ZasobyDokumentu.Add(damZasob);
                    }

                    foreach (DokumentHandlowy dokPow in dok.Nadrzędne)
                    {
                        DamObrotyWgDokumentowPowiazany dokumentPowiazany = new DamObrotyWgDokumentowPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamObrotyWgDokumentowId = dok.ID;

                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
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
                        DamObrotyWgDokumentowPowiazany dokumentPowiazany = new DamObrotyWgDokumentowPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamObrotyWgDokumentowId = dok.ID;

                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
                        if (dokumentPowiazany.Kontrahent != null)
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                        dokumentPowiazany.Netto = dokPow.Suma.Netto;
                        dokumentPowiazany.VAT = dokPow.Suma.VAT;
                        dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                        dokumentPowiazany.Waluta = dokPow.Suma.BruttoCy.Symbol;

                        damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    }
                    obroty.Add(damDokument);
                }
                return obroty;
            }
        }
    }
}