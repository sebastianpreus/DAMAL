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
                        towar = new Towar();
                        tm.Towary.AddRow(towar);
                    }
                    FillTowar(towar, damTowar);
                    trans.Commit();
                }
                session.Save();
            }
        }

        internal void PutTowar(DamTowar damTowar)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);
                using (ITransaction trans = session.Logout(true))
                {
                    Towar towar = (Towar)tm.Towary.WgEAN[damTowar.EAN].GetNext();
                    if (towar != null)
                    {
                        FillTowar(towar, damTowar);
                    }
                    trans.Commit();
                }
                session.Save();
            }
        }

        private void FillTowar(Towar towar, DamTowar damTowar)
        {
            towar.Kod = damTowar.Kod;
            //todo sprawdzić jak uzupełnić typ towaru
            //towar.Typ = TypTowaru.Produkt 
            towar.Nazwa = damTowar.Nazwa;
            towar.EAN = damTowar.EAN;
            towar.NumerKatalogowy = damTowar.NumerKatalogowy;
            //todo stawka vat sprzedaży i zakupu
            //towar.DefinicjaStawki = damTowar.DefinicjaStawkiVATSprzedazy;
            //towar.Jednostka = damTowar.Jednostka;
            towar.PKWiU = damTowar.PKWiU;


        }
    }
}