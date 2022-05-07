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
    public class ZamowieniaOdbiorcyService
    {
        public List<DamZamowienieOdbiorcy> GetZamowieniaOdbiorcy(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                DateTime start = DateTime.Now;
                int count = 0;

                List<DamZamowienieOdbiorcy> dokumenty = new List<DamZamowienieOdbiorcy>();

                HandelModule hamodule = HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Zamówienie odbiorcy");

                foreach (DokumentHandlowy dok in view1)
                {
                    DamZamowienieOdbiorcy damDokument = new DamZamowienieOdbiorcy();
                    damDokument.PozycjeDokumentu = new List<DamZamowienieOdbiorcyPozycja>();
                    damDokument.DokumentyPowiazane = new List<DamZamowienieOdbiorcyPowiazany>();
                    damDokument.ZasobyDokumentu = new List<DamZamowienieOdbiorcyZasob>();

                    damDokument.ID = dok.ID;
                    damDokument.Typ = dok.Definicja.Symbol;
                    damDokument.Zatwierdzony = dok.Zatwierdzony;
                    damDokument.Anulowany = dok.Anulowany;
                    damDokument.Potwierdzenie = dok.Potwierdzenie.ToString();
                    damDokument.Numer = dok.Numer.NumerPelny;
                    damDokument.Data = dok.Data;
                    damDokument.Podrzedne = dok.PodrzędneInfo;
                    damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                    damDokument.Netto = dok.Suma.Netto;
                    damDokument.Wartosc = dok.Suma.Brutto;
                    damDokument.Opis = dok.Opis;
                    //damDokument.StanPokrycia = Workers.StanPokryciaZamówienia.StanPokrycia
                    damDokument.ZaliczkaPokrywaCalosc = dok.Wydruk.ZaliczkaPokrywaCałość;
                    damDokument.Waluta = dok.Suma.BruttoCy.Symbol;

                    //damDokument.Priorytet = dok.ParametryRezerwacjiProxy.Priorytet.ToString();

                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamZamowienieOdbiorcyPozycja pozycja = new DamZamowienieOdbiorcyPozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamZamowienieOdbiorcyId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Rabat = poz.Rabat.ToString();
                        pozycja.CenaPoRabacie = poz.CenaPoRabacie.Value;
                        pozycja.Wartosc = poz.Wartość;
                        pozycja.StavkaVAT = poz.DefinicjaStawki.ToStringValue();
                        //pozycja.PozostaloIloscWZ = 
                        //pozycja.PozostaloIloscFV = poz.
                        //pozycja.StanPokryciaPozycji = poz.
                        pozycja.Waluta = poz.Suma.BruttoCy.Symbol;
                        if (poz.ParametryRezerwacji.Priorytet != null)
                            pozycja.Priorytet = poz.ParametryRezerwacji.Priorytet.ToString();
                        if (poz.ParametryRezerwacji.DataDo.Year < 9999)
                        {
                            pozycja.DataOd = poz.ParametryRezerwacji.DataOd;
                            pozycja.DataDo = poz.ParametryRezerwacji.DataDo;
                            pozycja.CzasOd = poz.ParametryRezerwacji.CzasOd.ToString();
                            pozycja.CzasDo = poz.ParametryRezerwacji.CzasDo.ToString();
                        }

                        damDokument.PozycjeDokumentu.Add(pozycja);
                    }

                    foreach (Zasob zasob in dok.ZasobyWszystkie)
                    {
                        DamZamowienieOdbiorcyZasob damZasob = new DamZamowienieOdbiorcyZasob();
                        damZasob.ID = zasob.ID;
                        damZasob.DamZamowienieOdbiorcyId = dok.ID;

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
                        DamZamowienieOdbiorcyPowiazany dokumentPowiazany = new DamZamowienieOdbiorcyPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamZamowienieOdbiorcyId = dok.ID;

                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
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
                        DamZamowienieOdbiorcyPowiazany dokumentPowiazany = new DamZamowienieOdbiorcyPowiazany();
                        dokumentPowiazany.ID = dokPow.ID;
                        dokumentPowiazany.DamZamowienieOdbiorcyId = dok.ID;

                        dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                        dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                        dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
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

        internal void DeleteZamowienieOdbiorcy(int id)
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

        internal void PostZamowienieOdbiorcy(DamZamowienieOdbiorcy damZamowienieOdbiorcy)
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

                    DefDokHandlowego definicja = hm.DefDokHandlowych.WgSymbolu[damZamowienieOdbiorcy.Typ];
                    if (definicja == null)
                        throw new InvalidOperationException("Nieznaleziona definicja dokumentu " + damZamowienieOdbiorcy.Typ);

                    if (damZamowienieOdbiorcy.ID > 0)
                    {
                        dokument = hm.DokHandlowe[damZamowienieOdbiorcy.ID];
                        dokument.Stan = StanDokumentuHandlowego.Bufor;
                        foreach (var poz in dokument.Pozycje)
                        {
                            poz.Delete();
                        }
                    }
                    else
                        hm.DokHandlowe.AddRow(dokument);

                    dokument.Definicja = definicja;

                    dokument.Magazyn = mm.Magazyny.WgNazwa[damZamowienieOdbiorcy.Magazyn];
                    dokument.Data = damZamowienieOdbiorcy.Data;
                    hm.DokHandlowe.AddRow(dokument);

                    if (damZamowienieOdbiorcy.Kontrahent != null)
                    {
                        Kontrahent kontrahent = cm.Kontrahenci.WgKodu[damZamowienieOdbiorcy.Kontrahent];
                        if (kontrahent == null)
                            throw new InvalidOperationException("Nieznaleziony kontrahent o kodzie " + damZamowienieOdbiorcy.Kontrahent);
                        dokument.Kontrahent = kontrahent;
                    }
                    
                    foreach (var damPozycja in damZamowienieOdbiorcy.PozycjeDokumentu)
                    {
                        Towar towar = (Towar)tm.Towary.WgKodu[damPozycja.Towar];
                        if (towar != null)
                        {
                            PozycjaDokHandlowego pozycja = new PozycjaDokHandlowego(dokument);
                            hm.PozycjeDokHan.AddRow(pozycja);
                            pozycja.Towar = towar;

                            pozycja.Ilosc = new Quantity(damPozycja.Ilosc, null);
                            // pozycja.Ilosc = new Quantity(10, "m"); //podana jednostka miary w metrach

                            pozycja.Cena = new DoubleCy(damPozycja.Cena);
                        }
                    }

                    foreach (SlownikElem sl in core.Slowniki.WgNazwa)
                    {
                        if (sl.Kategoria == "PriorytetZamAlg")
                        {
                            if (sl.Nazwa == damZamowienieOdbiorcy.Priorytet)
                                dokument.ParametryRezerwacjiProxy.Priorytet = sl;
                        }
                    }

                    dokument.Stan = StanDokumentuHandlowego.Zatwierdzony;
                    trans.Commit();
                }
                session.Save();
            }
        }
    }
}