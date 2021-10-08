using DamEnovaWebApi.Enova;
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
    public class PrzyjeciaMagazynoweService
    {
        public List<DamPrzyjecieMagazynowe> GetPrzyjeciaMagazynowe(int? id = null)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamPrzyjecieMagazynowe> dokumenty = new List<DamPrzyjecieMagazynowe>();

                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();

                view1.Condition = new FieldCondition.Equal("Kategoria", "Przyjęcie magazynowe");
                if (id != null)
                    view1.Condition &= new FieldCondition.Equal("ID", id);

                foreach (DokumentHandlowy dok in view1)
                {
                    DamPrzyjecieMagazynowe damDokument = new DamPrzyjecieMagazynowe();
                    damDokument.PozycjeDokumentu = new List<DamPrzyjecieMagazynowePozycja>();
                    damDokument.DokumentyPowiazane = new List<DamPrzyjecieMagazynowePowiazany>();
                    damDokument.ZasobyDokumentu = new List<DamPrzyjecieMagazynoweZasob>();

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
                    damDokument.Wartosc = dok.BruttoCy.Value;
                    damDokument.Waluta = dok.BruttoCy.Symbol;

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamPrzyjecieMagazynowePozycja pozycja = new DamPrzyjecieMagazynowePozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamPrzyjecieMagazynoweId = dok.ID;

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
                        DamPrzyjecieMagazynoweZasob damZasob = new DamPrzyjecieMagazynoweZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamPrzyjecieMagazynoweId = dok.ID;

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
                        DamPrzyjecieMagazynowePowiazany dokumentPowiazany = new DamPrzyjecieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamPrzyjecieMagazynoweId = dok.ID;

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
                        DamPrzyjecieMagazynowePowiazany dokumentPowiazany = new DamPrzyjecieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamPrzyjecieMagazynoweId = dok.ID;

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
