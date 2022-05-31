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
            Towar towar = new Towar();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);
                using (ITransaction trans = session.Logout(true))
                {
                    if (damTowar.ID > 0)
                    {
                        towar = tm.Towary[damTowar.ID];
                    }
                    else
                        tm.Towary.AddRow(towar);


                    towar.Kod = damTowar.Kod;
                    //todo sprawdzić jak uzupełnić typ towaru
                    //towar.Typ = TypTowaru.Produkt 
                    towar.Nazwa = damTowar.Nazwa;
                    towar.EAN = damTowar.EAN;
                    towar.NumerKatalogowy = damTowar.NumerKatalogowy;
                    //todo stawka vat sprzedaży i zakupu
                    //towar.DefinicjaStawki = damTowar.DefinicjaStawkiVATSprzedazy;

                    towar.Jednostka = tm.Jednostki.WgKodu[damTowar.Jednostka];
                    towar.PKWiU = damTowar.PKWiU;

                    towar.Features["T_TYP_SOP3"] = damTowar.T_TYP_SOP3;
                    towar.Features["T_ID_SOP3"] = damTowar.T_ID_SOP3;
                    towar.Features["T_NR_SOP3"] = damTowar.T_NR_SOP3;
                    towar.Features["T_Nr_rys"] = damTowar.T_Nr_rys;
                    towar.Features["T_Material_wyjsc"] = damTowar.T_Material_wyjsc;
                    towar.Features["T_SAP"] = damTowar.T_SAP;
                    towar.Features["T_Poz_kat_Bamet"] = damTowar.T_Poz_kat_Bamet;
                    towar.Features["T_Uwagi"] = damTowar.T_Uwagi;
                    towar.Features["T_Uwagi_wew"] = damTowar.T_Uwagi_wew;
                    towar.Features["T_Kontrolka"] = damTowar.T_Kontrolka;
                    towar.Features["T_Cecha"] = damTowar.T_Cecha;
                    towar.Features["T_Rodzaj_Kategoria"] = damTowar.T_Rodzaj_Kategoria;
                    towar.Features["T_Gatunek_Prod"] = damTowar.T_Gatunek_Prod;
                    towar.Features["T_Grupa"] = damTowar.T_Grupa;
                    //towar.Features["T_Rodzina"] = damTowar.T_Rodzina; //todo pojawia się błąd że nie może przyjmować wartości "1515" (jako string)
                    //DefinicjaCeny dfceny = new 
                    //DefinicjaCeny dfc = Soneta.Towary.DefinicjeCen
                    Cena cenaPodst = tm.Ceny.WgDefinicja[];

                    trans.Commit();
                }
                session.Save();
                damTowar.ID = towar.ID;
            }
        }

        internal void DeleteBlokadaTowaru(int id)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                TowaryModule tm = TowaryModule.GetInstance(session);

                using (ITransaction trans = session.Logout(true))
                {
                    Towar towar = new Towar();

                    if (id > 0)
                    {
                        towar = tm.Towary[id];
                        towar.Blokada = true;
                    }
                    trans.Commit();
                }
                session.Save();
            }
        }
    }
}