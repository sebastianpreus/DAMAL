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
    public class PrzyjeciaMagazynoweService
    {
        public List<DamPrzyjecieMagazynowe> GetPrzyjeciaMagazynowe(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                DateTime start = DateTime.Now;
                int count = 0;
                List<DamPrzyjecieMagazynowe> dokumenty = new List<DamPrzyjecieMagazynowe>();

                Soneta.Handel.HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Przyjęcie magazynowe");

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
                    {
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                        damDokument.KontrahentKod = dok.Kontrahent.Kod;
                        damDokument.KontrahentID = dok.Kontrahent.ID;
                    }
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.BruttoCy.Value;
                    damDokument.Waluta = dok.BruttoCy.Symbol;

                    //CECHY
                    damDokument.DH_TYP_SOP3 = dok.Features["DH_TYP_SOP3"].ToString();
                    damDokument.DH_ID_SOP3 = (int)dok.Features["DH_ID_SOP3"];
                    damDokument.DH_NR_SOP3 = dok.Features["DH_NR_SOP3"].ToString();

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamPrzyjecieMagazynowePozycja pozycja = new DamPrzyjecieMagazynowePozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamPrzyjecieMagazynoweId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.TowarKod = poz.Towar.Kod;
                        pozycja.TowarID = poz.Towar.ID;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Wartosc = poz.WartoscCy.Value;
                        pozycja.Waluta = poz.WartoscCy.Symbol;

                        //CECHY
                        pozycja.PDH_TYP_SOP3 = poz.Features["PDH_TYP_SOP3"].ToString();
                        pozycja.PDH_ID_SOP3 = (int)poz.Features["PDH_ID_SOP3"];
                        pozycja.PDH_NR_SOP3 = poz.Features["PDH_NR_SOP3"].ToString();
                        pozycja.PDH_ZP_NrDet_SOP3 = poz.Features["PDH_ZP_NrDet_SOP3"].ToString();
                        pozycja.PDH_WZ_SOP3 = poz.Features["PDH_WZ_SOP3"].ToString();
                        pozycja.PDH_ZO_SOP3 = poz.Features["PDH_ZO_SOP3"].ToString();
                        pozycja.PDH_ZP_SOP3 = poz.Features["PDH_ZP_SOP3"].ToString();

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamPrzyjecieMagazynoweZasob damZasob = new DamPrzyjecieMagazynoweZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamPrzyjecieMagazynoweId = dok.ID;

                        damZasob.OkresMagazynowy = zasob.Okres.ToStringValue();
                        damZasob.Towar = zasob.Towar.Nazwa;
                        damZasob.TowarKod = zasob.Towar.Kod;
                        damZasob.TowarID = zasob.Towar.ID;
                        damZasob.Typ = zasob.Partia.Typ.ToString();
                        damZasob.IloscZasobu = zasob.IlośćZasobu.Value;
                        damZasob.JednostkaMiary = zasob.IlośćZasobu.Symbol;
                        damZasob.Wartosc = zasob.Partia.Wartosc;
                        damZasob.Cena = zasob.Partia.Cena;
                        damZasob.DokumentPartia = zasob.Partia.Dokument.Numer.NumerPelny;
                        damZasob.DokumentPartiaPierwotna = zasob.PartiaPierwotna.Dokument.Numer.NumerPelny;

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
                        {
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                            dokumentPowiazany.KontrahentKod = dokPow.Kontrahent.Kod;
                            dokumentPowiazany.KontrahentID = dokPow.Kontrahent.ID;
                        }
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
                        {
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                            dokumentPowiazany.KontrahentKod = dokPow.Kontrahent.Kod;
                            dokumentPowiazany.KontrahentID = dokPow.Kontrahent.ID;
                        }
                        dokumentPowiazany.Netto = dokPow.Suma.Netto;
                        dokumentPowiazany.VAT = dokPow.Suma.VAT;
                        dokumentPowiazany.Wartosc = dokPow.Suma.Brutto;
                        dokumentPowiazany.Waluta = dokPow.Suma.BruttoCy.Symbol;

                        damDokument.DokumentyPowiazane.Add(dokumentPowiazany);
                    }
                    count += 1;
                    dokumenty.Add(damDokument);
                }
                var ttttttt = DateTime.Now - start;
                var ilosc = count;
                return dokumenty;
            }
        }

        internal void DeletePrzyjecieMagazynowe(int id)
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

                    if (id > 0)
                    {
                        dokument = hm.DokHandlowe[id];
                        dokument.Stan = StanDokumentuHandlowego.Bufor;
                        dokument.Delete();
                    }
                    trans.Commit();
                }
                session.Save();
            }
        }

        public void PostPrzyjeciaMagazynowe(DamPrzyjecieMagazynowe damPrzyjecieMagazynowe)
        {
            DokumentHandlowy dokument = new DokumentHandlowy();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                HandelModule hm = HandelModule.GetInstance(session);
                TowaryModule tm = TowaryModule.GetInstance(session);
                MagazynyModule mm = MagazynyModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);
                CoreModule core = CoreModule.GetInstance(session);

                using (ITransaction trans = session.Logout(true))
                {
                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu[damPrzyjecieMagazynowe.Typ];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu " + damPrzyjecieMagazynowe.Typ);

                    if (damPrzyjecieMagazynowe.ID > 0)
                    {
                        dokument = hm.DokHandlowe[damPrzyjecieMagazynowe.ID];
                        dokument.Stan = StanDokumentuHandlowego.Bufor;
                        foreach (var poz in dokument.Pozycje)
                        {
                            poz.Delete();
                        }
                    }
                    else
                        hm.DokHandlowe.AddRow(dokument);

                    dokument.Definicja = definicja;

                    dokument.Magazyn = mm.Magazyny.WgNazwa[damPrzyjecieMagazynowe.Magazyn];
                    dokument.Data = damPrzyjecieMagazynowe.Data;

                    //CECHY
                    dokument.Features["DH_TYP_SOP3"] = damPrzyjecieMagazynowe.DH_TYP_SOP3;
                    dokument.Features["DH_ID_SOP3"] = damPrzyjecieMagazynowe.DH_ID_SOP3;
                    dokument.Features["DH_NR_SOP3"] = damPrzyjecieMagazynowe.DH_NR_SOP3;

                    Kontrahent kontrahent = cm.Kontrahenci.WgKodu[damPrzyjecieMagazynowe.KontrahentKod];
                    if (kontrahent == null)
                        throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie " + damPrzyjecieMagazynowe.Kontrahent);
                    dokument.Kontrahent = kontrahent;


                    foreach (var damPozycja in damPrzyjecieMagazynowe.PozycjeDokumentu)
                    {
                        Towar towar = (Towar)tm.Towary.WgKodu[damPozycja.TowarKod];
                        if (towar != null)
                        {
                            PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                            hm.PozycjeDokHan.AddRow(pozycja);

                            pozycja.Towar = towar;
                            pozycja.Ilosc = new Quantity(damPozycja.Ilosc);
                            pozycja.Cena = new DoubleCy(damPozycja.Cena);
                            
                            //CECHY
                            pozycja.Features["PDH_TYP_SOP3"] = damPozycja.PDH_TYP_SOP3;
                            pozycja.Features["PDH_ID_SOP3"] = damPozycja.PDH_ID_SOP3;
                            pozycja.Features["PDH_NR_SOP3"] = damPozycja.PDH_NR_SOP3;
                            pozycja.Features["PDH_ZP_NrDet_SOP3"] = damPozycja.PDH_ZP_NrDet_SOP3;
                            pozycja.Features["PDH_WZ_SOP3"] = damPozycja.PDH_WZ_SOP3;
                            pozycja.Features["PDH_ZO_SOP3"] = damPozycja.PDH_ZO_SOP3;
                            pozycja.Features["PDH_ZP_SOP3"] = damPozycja.PDH_ZP_SOP3;
                        }

                    }

                    dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;
                    trans.Commit();
                }
                session.Save();
                damPrzyjecieMagazynowe.ID = dokument.ID;
            }
        }
    }
}
