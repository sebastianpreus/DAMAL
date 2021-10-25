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
    public class StanyMagazynoweService
    {
        public List<DamStanMagazynowy> GetStanyMagazynowe(string nazwaMagazynu = null)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamStanMagazynowy> stanyMagazynowe = new List<DamStanMagazynowy>();

                MagazynyModule mm = MagazynyModule.GetInstance(session);
                Magazyny mags = mm.Magazyny;
                foreach (Magazyn m in mags.WgNazwa)
                {
                    var magNazwa = m.Nazwa;
                    var magSymbol = m.Symbol;
                }

                TowaryModule tm = TowaryModule.GetInstance(session);
                Towary towary = tm.Towary;

                StanMagazynuWorker stanMag = new StanMagazynuWorker();

                foreach (Towar towar in towary.WgNazwy)
                {
                    
                    stanMag.Towar = towar;
                    stanMag.Magazyn = mm.Magazyny.WgNazwa[nazwaMagazynu];
                    stanMag.Data = DateTime.Now;
                    DamStanMagazynowy stanMagazynowy = new DamStanMagazynowy();

                    stanMagazynowy.Kod = towar.Kod;
                    stanMagazynowy.NazwaTowaru = towar.Nazwa;
                    stanMagazynowy.EAN = towar.EAN;
                    stanMagazynowy.StanRazem = stanMag.StanRazem;
                    stanMagazynowy.StanZamówien = stanMag.StanZamówień;
                    //Podstawowa/N
                    //Hurtowa/N
                    //Detaliczna/B
                    //ProcentVAT
                    //DoProdukcji
                    //dystrybutor
                    //EKSPLOATACJA
                    
                    
                    //stanMagazynowy.StanMagazynu = stanMag.StanMagazynu.Value;
                    //stanMagazynowy.WartoscMagazynu = stanMag.WartośćMagazynu;
                    //stanMagazynowy.WartoscKsiegowaMagazynu = stanMag.WartośćKsięgowaMagazynu;
                    
                    stanyMagazynowe.Add(stanMagazynowy);
                }
                return stanyMagazynowe;
            }
        }
    }
}