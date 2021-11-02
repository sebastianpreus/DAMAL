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

                TowaryModule tm = TowaryModule.GetInstance(session);
                Towary towary = tm.Towary;

                StanMagazynuWorker stanMag = new StanMagazynuWorker();
                StanRezerwacjiWorker stanRez = new StanRezerwacjiWorker();


                foreach (Towar towar in towary.WgNazwy)
                {
                    
                    stanMag.Towar = towar;
                    stanMag.Magazyn = mm.Magazyny.WgNazwa[nazwaMagazynu];
                    stanMag.Data = DateTime.Now;
                    DamStanMagazynowy stanMagazynowy = new DamStanMagazynowy();

                    //pola z enova i dokumentu word (rysunek 3)
                    stanMagazynowy.Kod = towar.Kod;
                    stanMagazynowy.Nazwa = towar.Nazwa;
                    stanMagazynowy.StanZamowien = stanMag.StanZamówień.Value;
                    stanMagazynowy.StanProdukcji = stanMag.StanProdukcji.Value;
                    stanMagazynowy.StanZapotrzebowaniaNaSurowce = stanMag.StanZapotrzebowaniaNaSurowce.Value;
                    stanMagazynowy.StanMinus = stanMag.StanMinus.Value;
                    stanMagazynowy.WartoscMagazynu = stanMag.WartośćMagazynu;
                    stanMagazynowy.StanKsięgowy = stanMag.StanKsięgowyMagazynu.Value;
                    stanMagazynowy.WartoscKsiegowa = stanMag.WartośćKsięgowaMagazynu;
                    try { stanMagazynowy.Hurtowa = towar.Ceny["Hurtowa"].Netto.Value; } catch (Exception) { }
                    try { stanMagazynowy.Narzut = ((double)stanMag.WgCeny["Hurtowa"].NarzutProcent); } catch (Exception) { }

                    //pozostałe pola (w word rysunek 2) : 
                    stanMagazynowy.EAN = towar.EAN;
                    stanMagazynowy.StanRazem = stanMag.StanRazem.Value;
                    try { stanMagazynowy.Podstawowa = towar.Ceny["Podstawowa"].Netto.Value; } catch (Exception) { }
                    try { stanMagazynowy.Detaliczna = towar.Ceny["Detaliczna"].Brutto.Value; } catch (Exception) { }

                    //pozostałe: 
                    stanMagazynowy.ProcentVAT = ((double)towar.ProcentVAT);
                    stanMagazynowy.StanMagazynu = stanMag.StanMagazynu.Value;
                    stanMagazynowy.WartoscKsiegowaMagazynu = stanMag.WartośćKsięgowaMagazynu;
                    stanMagazynowy.SredniaCenaZakupu = stanMag.CenaŚrednioważona;
                    try { stanMagazynowy.WartoscHurtowa = stanMag.WgCeny["Hurtowa"].WartośćNetto; } catch (Exception) { }

                    stanRez.Towar = towar;
                    stanRez.Magazyn = mm.Magazyny.WgNazwa[nazwaMagazynu];
                    stanRez.Data = DateTime.Now;

                    stanMagazynowy.Zarezerwowano = stanRez.IloscRezerwowana.Value;
                    stanMagazynowy.IloscDostepna = stanRez.IloscDostepna.Value;

                    //DoProdukcji
                    //dystrybutor
                    //EKSPLOATACJA

                    stanyMagazynowe.Add(stanMagazynowy);
                }
                return stanyMagazynowe;
            }
        }
    }
}