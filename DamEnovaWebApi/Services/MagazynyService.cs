using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using System;
using System.Collections.Generic;

namespace DamEnovaWebApi.Services
{
    public class MagazynyService
    {
        public List<DamMagazyn> GetMagazyny()
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamMagazyn> magazyny = new List<DamMagazyn>();

                MagazynyModule mm = MagazynyModule.GetInstance(session);
                Magazyny mags = mm.Magazyny;

                foreach (Magazyn m in mags.WgNazwa)
                {
                    magazyny.Add(new DamMagazyn()
                    {
                        ID = m.ID,
                        Nazwa = m.Nazwa,
                        Symbol = m.Symbol
                    });
                }

                return magazyny;
            }
        }
    }
}