using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Magazyny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class ZasobyService
    {
        public List<DamZasob> GetZasoby(int? id = null)
        {
            int count = 0;

            //DamalEnova damalEnova = new DamalEnova();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                MagazynyModule mg = MagazynyModule.GetInstance(session);
                Zasoby zasoby = mg.Zasoby;
                SubTable zasobySub = mg.Zasoby.WgMagazyn;

                if (id != null)
                {
                    RowCondition condition = new FieldCondition.Equal("ID", id);
                    zasobySub = zasobySub[condition];
                }
                List<DamZasob> damZasoby = new List<DamZasob>();
                DateTime start = DateTime.Now;

                foreach (Zasob zasob in zasobySub)
                {
                    count += 1;

                    DamZasob damZasob = new DamZasob();

                    damZasob.ID = zasob.ID;
                    damZasob.Kod = zasob.Towar.Kod;
                    damZasob.Nazwa = zasob.Towar.Nazwa;
                    damZasob.Ilosc = zasob.IlośćZasobu.Value;
                    damZasob.JednostkaMiary = zasob.IlośćZasobu.Symbol;
                    damZasob.Wartosc = zasob.Partia.Wartosc;
                    damZasob.Cena = zasob.Partia.Cena;
                    damZasob.Dokument = zasob.Partia.Dokument.Numer.NumerPelny;
                    damZasob.Data = zasob.Partia.Dokument.Data;
                    if (zasob.PartiaPierwotna.KontrahentPartii != null)
                    {
                        damZasob.Kontrahent = zasob.PartiaPierwotna.KontrahentPartii.Nazwa;
                        damZasob.KontrahentKod = zasob.PartiaPierwotna.KontrahentPartii.Kod;
                        damZasob.KontrahentID = zasob.PartiaPierwotna.KontrahentPartii.ID;
                    }
                    damZasob.Typ = zasob.Partia.Typ.ToString();
                    damZasob.TypTowaru = zasob.Towar.Typ.ToString();


                    damZasoby.Add(damZasob);
                }
                var ttttttt = DateTime.Now - start;
                var ilosc = count;


                return damZasoby;
            }
        }
    }
}