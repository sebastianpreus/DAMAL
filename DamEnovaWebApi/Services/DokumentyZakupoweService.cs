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
                //DateTime start = DateTime.Now;
                //int count = 0;
                List<DamDokumentZakupowy> dokumenty = new List<DamDokumentZakupowy>();

                HandelModule hamodule = Soneta.Handel.HandelModule.GetInstance(session);
                string _typDokumentu = "ZK";
                DefDokHandlowego def = hamodule.DefDokHandlowych.WgSymbolu[_typDokumentu];
                View view1 = hamodule.DokHandlowe.CreateView();

                filter.FilterView(view1);
                view1.Condition &= new FieldCondition.Equal("Kategoria", "Zakup");

                foreach (DokumentHandlowy dok in view1)
                {
                    //count += 1;
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
                    if (dok.Kontrahent != null)
                    {
                        damDokument.Kontrahent = dok.Kontrahent.Nazwa;
                        damDokument.KontrahentKod = dok.Kontrahent.Kod;
                        damDokument.KontrahentID = dok.Kontrahent.ID;
                    }
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
                        pozycja.TowarKod = poz.Towar.Kod;
                        pozycja.TowarID = poz.Towar.ID;
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
                        {
                            dokumentPowiazany.Kontrahent = dokPow.Kontrahent.Nazwa;
                            damDokument.KontrahentKod = dok.Kontrahent.Kod;
                            damDokument.KontrahentID = dok.Kontrahent.ID;
                        }
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

                //var ttttttt = DateTime.Now - start;
                //var ilosc = count;
                return dokumenty;
            }
        }
    }
}