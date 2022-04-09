using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Core;
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
    public class PrzesunieciaMagazynoweService
    {
        public List<DamPrzesuniecieMagazynowe> GetPrzesuniecieMagazynowe(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamPrzesuniecieMagazynowe> dokumenty = new List<DamPrzesuniecieMagazynowe>();

                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Przesunięcie magazynowe");

                foreach (DokumentHandlowy dok in view1)
                {
                    DamPrzesuniecieMagazynowe damDokument = new DamPrzesuniecieMagazynowe();
                    damDokument.PozycjeDokumentu = new List<DamPrzesuniecieMagazynowePozycja>();
                    damDokument.DokumentyPowiazane = new List<DamPrzesuniecieMagazynowePowiazany>();
                    damDokument.ZasobyDokumentu = new List<DamPrzesuniecieMagazynoweZasob>();

                    damDokument.ID = dok.ID;
                    damDokument.Typ = dok.Definicja.Symbol;
                    damDokument.Zatwierdzony = dok.Zatwierdzony;
                    damDokument.Numer = dok.Numer.NumerPelny;
                    damDokument.Data = dok.Data;
                    if (dok.MagazynDo != null)
                        damDokument.DoMagazynu = dok.MagazynDo.ToStringValue();
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.BruttoCy.Value;
                    damDokument.Waluta = dok.BruttoCy.Symbol;

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamPrzesuniecieMagazynowePozycja pozycja = new DamPrzesuniecieMagazynowePozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamPrzesuniecieMagazynoweId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.TowarKod = poz.Towar.Kod;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.WartoscCy.Value;
                        pozycja.Waluta = poz.WartoscCy.Symbol;

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamPrzesuniecieMagazynoweZasob damZasob = new DamPrzesuniecieMagazynoweZasob();

                        damZasob.ID = zasob.ID;
                        damZasob.DamPrzesuniecieMagazynoweId = dok.ID;

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
                        DamPrzesuniecieMagazynowePowiazany dokumentPowiazany = new DamPrzesuniecieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamPrzesuniecieMagazynoweId = dok.ID;

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
                        DamPrzesuniecieMagazynowePowiazany dokumentPowiazany = new DamPrzesuniecieMagazynowePowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamPrzesuniecieMagazynoweId = dok.ID;

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

        public void PostPrzesuniecieMagazynowe(DamPrzesuniecieMagazynowe damPrzesuniecieMagazynowe)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                HandelModule hm = HandelModule.GetInstance(session);
                TowaryModule tm = TowaryModule.GetInstance(session);
                MagazynyModule mm = MagazynyModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);
                CoreModule core = CoreModule.GetInstance(session);

                using (ITransaction trans = session.Logout(true))
                {
                    DokumentHandlowy dokument = new DokumentHandlowy();

                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu[damPrzesuniecieMagazynowe.Typ];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu " + damPrzesuniecieMagazynowe.Typ);
                    dokument.Definicja = definicja;

                    dokument.Magazyn = mm.Magazyny.WgNazwa[damPrzesuniecieMagazynowe.Magazyn];
                    dokument.Data = damPrzesuniecieMagazynowe.Data;
                    hm.DokHandlowe.AddRow(dokument);

                    //Kontrahent kontrahent = cm.Kontrahenci.WgKodu[damPrzesuniecieMagazynowe.KontrahentKod];
                    //if (kontrahent == null)
                    //    throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie " + damPrzesuniecieMagazynowe.Kontrahent);
                    //dokument.Kontrahent = kontrahent;

                    foreach (var damPozycja in damPrzesuniecieMagazynowe.PozycjeDokumentu)
                    {
                        Towar towar = (Towar)tm.Towary.WgKodu[damPozycja.TowarKod];
                        if (towar != null)
                        {
                            PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                            hm.PozycjeDokHan.AddRow(pozycja);
                            pozycja.Towar = towar;

                            pozycja.Ilosc = new Quantity(damPozycja.Ilosc, null);
                            // pozycja.Ilosc = new Quantity(10, "m"); //podana jednostka miary w metrach

                            //pozycja.Cena = new DoubleCy(damPozycja.Cena);
                        }
                    }
                    foreach (SlownikElem sl in core.Slowniki.WgNazwa)
                    {
                        if (sl.Kategoria == "PriorytetZamAlg")
                        {
                            if (sl.Nazwa == damPrzesuniecieMagazynowe.Priorytet)
                                dokument.ParametryRezerwacjiProxy.Priorytet = sl;
                        }
                    }

                    //działa dla PW, dla RW nie chce... może trzeba zapisać i dopiero zmienić stan na zatwierdzony
                    //dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;

                    trans.Commit();
                }
                session.Save();
            }
        }
    }
}