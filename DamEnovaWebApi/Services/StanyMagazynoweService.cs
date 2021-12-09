using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
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
        public List<DamStanMagazynowy> GetStanyMagazynowe(Filter filter, string nazwaMagazynu = null)
        {
            DateTime start = DateTime.Now;
            int count = 0;

            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamStanMagazynowy> stanyMagazynowe = new List<DamStanMagazynowy>();

                MagazynyModule mm = MagazynyModule.GetInstance(session);

                TowaryModule tm = TowaryModule.GetInstance(session);
                Towary towary = tm.Towary;
                
                StanMagazynuWorker stanMag = new StanMagazynuWorker();
                StanRezerwacjiWorker stanRez = new StanRezerwacjiWorker();

                int skip = 0;
                foreach (Towar towar in towary.WgNazwy)
                {
                    if (skip >= filter.Skip)
                    {
                        count += 1;
                        stanMag.Towar = towar;
                        if (nazwaMagazynu != null)
                            stanMag.Magazyn = mm.Magazyny.WgNazwa[nazwaMagazynu];
                        //stanMag.Data = DateTime.Now;
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

                        foreach (Cena cena in towar.Ceny)
                        {
                            if (cena.Definicja.Nazwa == "Hurtowa")
                            {
                                stanMagazynowy.Hurtowa = cena.Netto.Value;
                                try { stanMagazynowy.Narzut = ((double)stanMag.WgCeny["Hurtowa"].NarzutProcent); } catch (Exception) { }
                                try { stanMagazynowy.WartoscHurtowa = stanMag.WgCeny["Hurtowa"].WartośćNetto; } catch (Exception) { }
                            }
                            if (cena.Definicja.Nazwa.ToLower() == "Podstawowa")
                            {
                                stanMagazynowy.Podstawowa = cena.Netto.Value;
                            }
                            if (cena.Definicja.Nazwa.ToLower() == "Detaliczna")
                            {
                                stanMagazynowy.Detaliczna = cena.Brutto.Value;
                            }
                        }

                        //try { stanMagazynowy.Hurtowa = towar.Ceny["Hurtowa"].Netto.Value; } catch (Exception) { }
                        //try { stanMagazynowy.Narzut = ((double)stanMag.WgCeny["Hurtowa"].NarzutProcent); } catch (Exception) { }

                        //pozostałe pola (w word rysunek 2) : 
                        stanMagazynowy.EAN = towar.EAN;
                        stanMagazynowy.StanRazem = stanMag.StanRazem.Value;

                        //try { stanMagazynowy.Podstawowa = towar.Ceny["Podstawowa"].Netto.Value; } catch (Exception) { }
                        //try { stanMagazynowy.Detaliczna = towar.Ceny["Detaliczna"].Brutto.Value; } catch (Exception) { }

                        //pozostałe: 
                        stanMagazynowy.ProcentVAT = ((double)towar.ProcentVAT);
                        stanMagazynowy.StanMagazynu = stanMag.StanMagazynu.Value;
                        stanMagazynowy.WartoscKsiegowaMagazynu = stanMag.WartośćKsięgowaMagazynu;
                        stanMagazynowy.SredniaCenaZakupu = stanMag.CenaŚrednioważona;

                        //try { stanMagazynowy.WartoscHurtowa = stanMag.WgCeny["Hurtowa"].WartośćNetto; } catch (Exception) { }

                        stanRez.Towar = towar;
                        if (nazwaMagazynu != null)
                            stanRez.Magazyn = mm.Magazyny.WgNazwa[nazwaMagazynu];
                        //stanRez.Data = DateTime.Now;

                        stanMagazynowy.Zarezerwowano = stanRez.IloscRezerwowana.Value;
                        stanMagazynowy.IloscDostepna = stanRez.IloscDostepna.Value;

                        //DoProdukcji
                        //dystrybutor
                        //EKSPLOATACJA

                        stanyMagazynowe.Add(stanMagazynowy);
                        if (filter.Top > 0 && filter.Top == stanyMagazynowe.Count)
                            break;
                    }
                    else
                        skip += 1;
                }
                var ttttttt = DateTime.Now - start;
                var ilosc = count;

                return stanyMagazynowe;
            }
        }
    }
}