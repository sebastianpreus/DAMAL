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
        public List<DamZasob> GetZasoby()
        {
            //DamalEnova damalEnova = new DamalEnova();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                MagazynyModule mg = MagazynyModule.GetInstance(session);

                Zasoby zasoby = mg.Zasoby;
                View mgview = zasoby.CreateView();

                List<DamZasob> damZasoby = new List<DamZasob>();
                foreach (Zasob zasob in mgview)
                {
                    DamZasob damZasob = new DamZasob();
                    damZasob.MapEnovaObject(zasob);
                    damZasoby.Add(damZasob);
                }
                return damZasoby;
            }
        }
    }
}