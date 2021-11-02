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
    public class WydaniaMagazynoweService
    {
        public List<DamWydanieMagazynowe> GetWydaniaMagazynowe(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamWydanieMagazynowe> dokumenty = new List<DamWydanieMagazynowe>();

                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Wydanie magazynowe");

                foreach (DokumentHandlowy dok in view1)
                {
                    DamWydanieMagazynowe damDokument = new DamWydanieMagazynowe();
                    damDokument.PozycjeDokumentu = new List<DamWydanieMagazynowePozycja>();
                    damDokument.DokumentyPowiazane = new List<DamWydanieMagazynowePowiazany>();
                    damDokument.ZasobyDokumentu = new List<DamWydanieMagazynoweZasob>();

                    damDokument.ID = dok.ID;
                    damDokument.Typ = dok.Definicja.Symbol;
                    damDokument.Zatwierdzony = dok.Zatwierdzony;
                    damDokument.Korekta = dok.Korekta;
                    damDokument.Numer = dok.Numer.NumerPelny;
                    damDokument.Data = dok.Data;
                    if (dok.Kontrahent != null)
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.Suma.BruttoCy.Value;
                    damDokument.Waluta = dok.Suma.BruttoCy.Symbol;

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamWydanieMagazynowePozycja pozycja = new DamWydanieMagazynowePozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamWydanieMagazynoweId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.WartoscCy.Value;
                        pozycja.Waluta = poz.WartoscCy.Symbol;

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamWydanieMagazynoweZasob damZasob = new DamWydanieMagazynoweZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamWydanieMagazynoweId = dok.ID;

                        damZasob.OkresMagazynowy = zasob.Okres.ToStringValue();
                        damZasob.Towar = zasob.Towar.ToStringValue();
                        damZasob.Typ = zasob.Partia.Typ.ToString();
                        damZasob.IloscZasobu = zasob.IlośćZasobu.Value;
                        damZasob.JednostkaMiary = zasob.IlośćZasobu.Symbol;
                        damZasob.Wartosc = zasob.Partia.Wartosc;
                        damZasob.Cena = zasob.Partia.Cena;

                        damDokument.ZasobyDokumentu.Add(damZasob);
                    }

                    foreach (DokumentHandlowy dokPow in dok.Nadrzędne)
                    {
                        DamWydanieMagazynowePowiazany dokumentPowiazany = new DamWydanieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamWydanieMagazynoweId = dok.ID;

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
                        DamWydanieMagazynowePowiazany dokumentPowiazany = new DamWydanieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamWydanieMagazynoweId = dokPow.ID;

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
                return dokumenty;
            }
        }
    }
}