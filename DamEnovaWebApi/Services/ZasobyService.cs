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
            //DamalEnova damalEnova = new DamalEnova();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                MagazynyModule mg = MagazynyModule.GetInstance(session);

                Zasoby zasoby = mg.Zasoby;
                View mgview = zasoby.CreateView();
                if (id != null)
                    mgview.Condition &= new FieldCondition.Equal("ID", id);

                List<DamZasob> damZasoby = new List<DamZasob>();
                foreach (Zasob zasob in mgview)
                {
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
                    //this.DaokumentPierw = zasob.Partia.WgDokument;
                    //this.Data = zasob.Partia.WgDokument;
                    if (zasob.PartiaPierwotna.KontrahentPartii != null)
                        damZasob.Dostawca = zasob.PartiaPierwotna.KontrahentPartii.Nazwa;
                    damZasob.Typ = zasob.Towar.Typ.ToString();


                    damZasoby.Add(damZasob);
                }
                return damZasoby;
            }
        }
    }
}