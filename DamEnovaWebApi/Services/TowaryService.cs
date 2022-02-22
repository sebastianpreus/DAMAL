using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
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
    public class TowaryService
    {
        internal List<DamTowar> GetTowary(Filter filter)
        {
            throw new NotImplementedException();
        }

        internal void PostTowar(DamTowar damTowar)
        {

            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);
                using (ITransaction trans = session.Logout(true))
                {
                    Towar towar = (Towar)tm.Towary.WgEAN[damTowar.EAN].GetNext();
                    if (towar == null)
                    {
                        Towar newTowar = new Towar();

                        tm.Towary.AddRow(newTowar);

                        newTowar.Kod = damTowar.Kod;
                        newTowar.Nazwa = damTowar.Nazwa;
                        newTowar.EAN = damTowar.EAN;

                    }
                    trans.Commit();
                }
                session.Save(); 
            }
        }
    }
}