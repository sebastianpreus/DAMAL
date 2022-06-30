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
    public class ZamowieniaOdbiorcyOdPozycjiService
    {
        public List<DamZamowienieOdbiorcyOdPozycjiPozycja> GetZamowieniaOdbiorcyOdPozycji(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamZamowienieOdbiorcyOdPozycjiPozycja> pozycje = new List<DamZamowienieOdbiorcyOdPozycjiPozycja>();

                HandelModule hamodule = HandelModule.GetInstance(session);
                View view1 = hamodule.DokHandlowe.CreateView();
                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Zamówienie odbiorcy");

                foreach (DokumentHandlowy dok in view1)
                {
                    foreach (PozycjaDokHandlowego poz in dok.Pozycje)
                    {
                        DamZamowienieOdbiorcyOdPozycjiPozycja pozycja = new DamZamowienieOdbiorcyOdPozycjiPozycja();
                        pozycja.ID = poz.ID;
                        pozycja.DamZamowienieOdbiorcyOdPozycjiId = dok.ID;

                        pozycja.Lp = poz.Lp;
                        pozycja.Towar = poz.Towar.Nazwa;
                        pozycja.TowarKod = poz.Towar.Kod;
                        pozycja.TowarID = poz.Towar.ID;
                        pozycja.Ilosc = poz.Ilosc.Value;
                        pozycja.JednostkaMiary = poz.Ilosc.Symbol;
                        pozycja.Cena = poz.Cena.Value;
                        pozycja.Rabat = poz.Rabat.ToString();
                        pozycja.CenaPoRabacie = poz.CenaPoRabacie.Value;
                        pozycja.Wartosc = poz.Wartość;
                        pozycja.StavkaVAT = poz.DefinicjaStawki.ToStringValue();
                        pozycja.Waluta = poz.Suma.BruttoCy.Symbol;
                        if (poz.ParametryRezerwacji.Priorytet != null)
                            pozycja.Priorytet = poz.ParametryRezerwacji.Priorytet.ToString();
                        if (poz.ParametryRezerwacji.DataDo.Year < 9999)
                        {
                            pozycja.DataOd = poz.ParametryRezerwacji.DataOd;
                            pozycja.DataDo = poz.ParametryRezerwacji.DataDo;
                        }
                        pozycja.DamZamowienieOdbiorcyOdPozycji = AddDamZamowienieOdbiorcyOdPozycji(dok);

                        pozycje.Add(pozycja);
                    }
                }
                return pozycje;
            }
        }

        DamZamowienieOdbiorcyOdPozycji AddDamZamowienieOdbiorcyOdPozycji(DokumentHandlowy dok)
        {

            DamZamowienieOdbiorcyOdPozycji damDokument = new DamZamowienieOdbiorcyOdPozycji();
            //damDokument.PozycjeDokumentu = new List<DamZamowienieOdbiorcyPozycja>();
            damDokument.DokumentyPowiazane = new List<DamZamowienieOdbiorcyOdPozycjiPowiazany>();
            damDokument.ZasobyDokumentu = new List<DamZamowienieOdbiorcyOdPozycjiZasob>();

            damDokument.ID = dok.ID;
            damDokument.Typ = dok.Definicja.Symbol;
            damDokument.Zatwierdzony = dok.Zatwierdzony;
            damDokument.Anulowany = dok.Anulowany;
            damDokument.Numer = dok.Numer.NumerPelny;
            damDokument.Data = dok.Data;
            damDokument.Kontrahent = dok.Kontrahent.Nazwa;
            damDokument.KontrahentKod = dok.Kontrahent.Kod;
            damDokument.KontrahentID = dok.Kontrahent.ID;
            damDokument.Netto = dok.Suma.Netto;
            damDokument.Wartosc = dok.Suma.Brutto;
            damDokument.VAT = dok.Suma.VAT;
            damDokument.Opis = dok.Opis;
            damDokument.Waluta = dok.Suma.BruttoCy.Symbol;

            foreach (Zasob zasob in dok.ZasobyWszystkie)
            {
                DamZamowienieOdbiorcyOdPozycjiZasob damZasob = new DamZamowienieOdbiorcyOdPozycjiZasob();
                damZasob.ID = zasob.ID;
                damZasob.DamZamowienieOdbiorcyOdPozycjiId = dok.ID;

                damZasob.OkresMagazynowy = zasob.Okres.ToStringValue();
                damZasob.Towar = zasob.Towar.Nazwa;
                damZasob.TowarKod = zasob.Towar.Kod;
                damZasob.TowarID = zasob.Towar.ID;
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
                DamZamowienieOdbiorcyOdPozycjiPowiazany dokumentPowiazany = new DamZamowienieOdbiorcyOdPozycjiPowiazany();
                dokumentPowiazany.ID = dokPow.ID;
                dokumentPowiazany.DamZamowienieOdbiorcyOdPozycjiId = dok.ID;

                dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
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
                DamZamowienieOdbiorcyOdPozycjiPowiazany dokumentPowiazany = new DamZamowienieOdbiorcyOdPozycjiPowiazany();
                dokumentPowiazany.ID = dokPow.ID;
                dokumentPowiazany.DamZamowienieOdbiorcyOdPozycjiId = dok.ID;

                dokumentPowiazany.Typ = dokPow.Definicja.Symbol;
                dokumentPowiazany.Numer = dokPow.Numer.NumerPelny;
                dokumentPowiazany.Zatwierdzony = dokPow.Zatwierdzony;
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
            return damDokument;
        }
    }
}