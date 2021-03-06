using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Core;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Handel.RelacjeDokumentow.Api;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
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
                    damDokument.DataOperacji = dok.DataOperacji;
                    if (dok.Kontrahent != null)
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.VAT = dok.Suma.VAT;
                    damDokument.Wartosc = dok.Suma.BruttoCy.Value;
                    damDokument.Waluta = dok.Suma.BruttoCy.Symbol;
                    damDokument.Opis = dok.Opis;

                    //CECHY
                    damDokument.DH_TYP_SOP3 = dok.Features["DH_TYP_SOP3"].ToString();
                    damDokument.DH_ID_SOP3 = (int)dok.Features["DH_ID_SOP3"];
                    damDokument.DH_NR_SOP3 = dok.Features["DH_NR_SOP3"].ToString();

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamWydanieMagazynowePozycja pozycja = new DamWydanieMagazynowePozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamWydanieMagazynoweId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Kod;
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
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Kod;
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
                    //if (dokumenty.Count == 500)
                    //    break;
                }
                return dokumenty;
            }
        }

        internal void PostWydaniaMagazynoweNaPodstawieZO(DamWydanieMagazynoweNaPodstawieZO damWydanieMagazynoweNaPodstawieZO)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                HandelModule hm = HandelModule.GetInstance(session);
                TowaryModule tm = TowaryModule.GetInstance(session);
                MagazynyModule mm = MagazynyModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);

                using (ITransaction trans = session.Logout(true))
                {
                    //
                    // Wyszukujemy dokument ZO, z którego ma być utworzony nowy dokument.
                    //
                    List<DokumentHandlowy> listaDokumentowZO = new List<DokumentHandlowy>();
                    foreach (string numerDokZO in damWydanieMagazynoweNaPodstawieZO.NumeryDokumentowZO)
                    {
                        DokumentHandlowy dokumentZO = hm.DokHandlowe.NumerWgNumeruDokumentu[numerDokZO];
                        if (dokumentZO == null)
                            throw new InvalidOperationException("Nie znaleziono dokumentu " + numerDokZO);
                        listaDokumentowZO.Add(dokumentZO);
                    }

                    //Tworzenie dokumentu 
                    var apiRelacje = (IRelacjeService)session.GetService(typeof(IRelacjeService));
                    DokumentHandlowy dokument = new DokumentHandlowy();
                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu[damWydanieMagazynoweNaPodstawieZO.Typ];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu " + damWydanieMagazynoweNaPodstawieZO.Typ);
                    dokument.Definicja = definicja;

                    foreach (DokumentHandlowy dokumentZO in listaDokumentowZO)
                    {
                        dokument = apiRelacje.NowyPodrzednyIndywidualny(new[] { dokumentZO }, damWydanieMagazynoweNaPodstawieZO.Typ).FirstOrDefault();
                    }

                    trans.Commit();
                }
                session.Save();
            }
            //dokument.Magazyn = mm.Magazyny.WgNazwa[damWydanieMagazynowe.Magazyn];
            //dokument.Data = damWydanieMagazynowe.Data;
            //hm.DokHandlowe.AddRow(dokument);
        }

        internal void PostWydaniaMagazynowe(DamWydanieMagazynowe damWydanieMagazynowe)
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
                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu[damWydanieMagazynowe.Typ];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu " + damWydanieMagazynowe.Typ);

                    if (damWydanieMagazynowe.ID > 0)
                    {
                        dokument = hm.DokHandlowe[damWydanieMagazynowe.ID];
                        dokument.Stan = StanDokumentuHandlowego.Bufor;
                        foreach (var poz in dokument.Pozycje)
                        {
                            poz.Delete();
                        }
                    }
                    else
                        hm.DokHandlowe.AddRow(dokument);

                    dokument.Definicja = definicja;

                    dokument.Magazyn = mm.Magazyny.WgNazwa[damWydanieMagazynowe.Magazyn];
                    dokument.Data = damWydanieMagazynowe.Data;
                    dokument.DataOperacji = damWydanieMagazynowe.DataOperacji;
                    dokument.Opis = damWydanieMagazynowe.Opis;

                    //CECHY
                    dokument.Features["DH_TYP_SOP3"] = damWydanieMagazynowe.DH_TYP_SOP3;
                    dokument.Features["DH_ID_SOP3"] = damWydanieMagazynowe.DH_ID_SOP3;
                    dokument.Features["DH_NR_SOP3"] = damWydanieMagazynowe.DH_NR_SOP3;


                    if (damWydanieMagazynowe.Kontrahent != null)
                    {
                        Kontrahent kontrahent = cm.Kontrahenci.WgKodu[damWydanieMagazynowe.Kontrahent];
                        if (kontrahent == null)
                            throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie " + damWydanieMagazynowe.Kontrahent);
                        dokument.Kontrahent = kontrahent;
                    }

                    foreach (var damPozycja in damWydanieMagazynowe.PozycjeDokumentu)
                    {
                        Towar towar = (Towar)tm.Towary.WgKodu[damPozycja.Towar];
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

                    try
                    {
                        foreach (SlownikElem sl in core.Slowniki.WgNazwa)
                        {
                            if (sl.Kategoria == "PriorytetZamAlg")
                            {
                                if (sl.Nazwa == damWydanieMagazynowe.Priorytet)
                                    dokument.ParametryRezerwacjiProxy.Priorytet = sl;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Błąd ustawiania priorytetu dokumentu");
                    }

                    dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;

                    trans.Commit();
                }
                session.Save();
                damWydanieMagazynowe.ID = dokument.ID;
            }
        }

        public void PostWydaniaMagazynoweKorekta(DamWydanieMagazynowe damWydanieMagazynowe)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                HandelModule hm = HandelModule.GetInstance(session);
                TowaryModule tm = TowaryModule.GetInstance(session);
                MagazynyModule mm = MagazynyModule.GetInstance(session);
                CRMModule cm = CRMModule.GetInstance(session);

                using (ITransaction trans = session.Logout(true))
                {
                    DokumentHandlowy dokument = hm.DokHandlowe[damWydanieMagazynowe.ID];

                    DefRelacjiKorekta defKorekta = (DefRelacjiKorekta)dokument.Definicja.RelacjaKorektyDefinicja;

                    if (defKorekta == null)
                        throw new InvalidOperationException("Dokument " + dokument + " nie ma zdefiniowanej relacji korekty.");

                    DokumentHandlowy korekta = defKorekta.KorygujDokument(dokument);

                    foreach (PozycjaDokHandlowego pozycja in korekta.Pozycje)
                    {
                        using (var transPozycji = session.Logout(true))
                        {
                            var damPozycja = damWydanieMagazynowe.PozycjeDokumentu.FirstOrDefault(x => x.Lp == pozycja.Lp);

                            //tutaj wszystkie zmiany w tym cena, rabat, ilosc
                            //pozycja.Cena = new DoubleCy(20.01);
                            pozycja.Ilosc = new Quantity(damPozycja.Ilosc, pozycja.Ilosc.Symbol);

                            //tutaj musimy zatwierdzic zmodyfikowana pozycje
                            transPozycji.CommitUI();
                        }
                    }
                    korekta.Stan = StanDokumentuHandlowego.Zatwierdzony;
                    trans.Commit();
                }
                session.Save();
            }
        }
    }
}