using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class KontrahenciService
    {
        public List<DamKontrahent> GetKontrahenci(int? id = null)
        {

            //DamalEnova damalEnova = new DamalEnova();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                DateTime start = DateTime.Now;
                int count = 0;

                CRMModule cm = CRMModule.GetInstance(session);
                Kontrahenci khlst = cm.Kontrahenci;
                View khview = khlst.CreateView();
                if (id != null)
                    khview.Condition &= new FieldCondition.Equal("ID", id);

                List<DamKontrahent> kontrahents = new List<DamKontrahent>();

                foreach (Kontrahent kontrahent in khview)
                {
                    DamKontrahent damKontrahent = new DamKontrahent();

                    damKontrahent.Kod = kontrahent.Kod;
                    damKontrahent.Nazwa = damKontrahent.Nazwa;
                    damKontrahent.NIP = damKontrahent.NIP;
                    damKontrahent.PodatnikVAT = damKontrahent.PodatnikVAT;
                    damKontrahent.StatusPodmiotu = kontrahent.StatusPodmiotu.ToString();
                    damKontrahent.FormaPrawna = kontrahent.FormaPrawna.ToString();

                    damKontrahent.DamAdres.Miejscowosc = kontrahent.Adres.Miejscowosc;
                    damKontrahent.DamAdres.Ulica = kontrahent.Adres.Ulica;
                    damKontrahent.DamAdres.NrDomu = kontrahent.Adres.NrDomu;
                    damKontrahent.DamAdres.NrLokalu = kontrahent.Adres.NrLokalu;
                    damKontrahent.DamAdres.KodPocztowy = kontrahent.Adres.KodPocztowy;
                    damKontrahent.DamAdres.Miejscowosc = kontrahent.Adres.Miejscowosc;
                    damKontrahent.DamAdres.Poczta = kontrahent.Adres.Poczta;
                    damKontrahent.DamAdres.Gmina = kontrahent.Adres.Gmina;
                    damKontrahent.DamAdres.Powiat = kontrahent.Adres.Powiat;

                    //todo - przekazac i uzupełnić województwo 
                    damKontrahent.DamAdres.Wojewodztwo = kontrahent.Adres.Wojewodztwo.ToString();
                    
                    damKontrahent.DamAdres.Kraj = kontrahent.Adres.Kraj;
                    damKontrahent.DamAdres.KodKraju = kontrahent.Adres.KodKraju;
                    damKontrahent.DamAdres.Telefon = kontrahent.Adres.Telefon;
                    damKontrahent.DamAdres.Faks = kontrahent.Adres.Faks;
                    damKontrahent.DamAdres.NietypowaLokalizacja = kontrahent.Adres.NietypowaLokalizacja;
                    damKontrahent.PESEL = kontrahent.PESEL;
                    damKontrahent.REGON = kontrahent.REGON;
                    damKontrahent.KRS = kontrahent.KRS;
                    // todo GLN/ILN
                    damKontrahent.EMAIL = kontrahent.EMAIL;

                    //CECHY
                    damKontrahent.K_NUMER_SOP3 = kontrahent.Features["K_NUMER_SOP3"].ToString();
                    damKontrahent.K_ID_SOP3 = (int)kontrahent.Features["K_NUMER_SOP3"];

                    kontrahents.Add(damKontrahent);
                    count += 1;
                }
                var ttttttt = DateTime.Now - start;
                var ilosc = count;
                return kontrahents;
            }
        }

        internal void PostKontrahent(DamKontrahent damKontrahent)
        {
            using (Session session1 = Connection.enovalogin.CreateSession(false, false))
            {
                CRMModule cm = CRMModule.GetInstance(session1);
                using (ITransaction trans = session1.Logout(true))
                {
                    Kontrahent kontrahent = (Kontrahent)cm.Kontrahenci.WgNIP[damKontrahent.NIP].GetNext();
                    if (kontrahent == null)
                    {
                        Kontrahent newKontrahent = new Kontrahent();
                        cm.Kontrahenci.AddRow(newKontrahent);

                        newKontrahent.Kod = damKontrahent.Kod;
                        newKontrahent.Nazwa = damKontrahent.Nazwa;
                        newKontrahent.NIP = damKontrahent.NIP;
                        newKontrahent.PodatnikVAT = damKontrahent.PodatnikVAT;
                        if (damKontrahent.StatusPodmiotu == "Finalny")
                            newKontrahent.StatusPodmiotu = Soneta.Core.StatusPodmiotu.Finalny;
                        else
                            newKontrahent.StatusPodmiotu = Soneta.Core.StatusPodmiotu.PodmiotGospodarczy;
                        //todo jak to wypełnić  ?
                        //newKontrahent.FormaPrawna = damKontrahent.FormaPrawna;

                        //newKontrahent.Adres.Ulica = damKontrahent.DamAdres.Linia1;
                        //newKontrahent.Adres.NrDomu = damKontrahent.DamAdres.Linia2;
                        //newKontrahent.Adres.NrLokalu = "34";
                        newKontrahent.Adres.Miejscowosc = damKontrahent.DamAdres.Miejscowosc;

                        newKontrahent.Adres.Ulica = damKontrahent.DamAdres.Ulica;
                        newKontrahent.Adres.NrDomu = damKontrahent.DamAdres.NrDomu;
                        newKontrahent.Adres.NrLokalu = damKontrahent.DamAdres.NrLokalu;
                        newKontrahent.Adres.KodPocztowy = damKontrahent.DamAdres.KodPocztowy;
                        newKontrahent.Adres.Miejscowosc = damKontrahent.DamAdres.Miejscowosc;
                        newKontrahent.Adres.Poczta = damKontrahent.DamAdres.Poczta;
                        newKontrahent.Adres.Gmina = damKontrahent.DamAdres.Gmina;
                        newKontrahent.Adres.Powiat = damKontrahent.DamAdres.Powiat;
                        
                        //todo - przekazac i uzupełnić województwo 
                        //newKontrahent.Adres.Wojewodztwo = damKontrahent.DamAdres.Wojewodztwo;
                        newKontrahent.Adres.Kraj = damKontrahent.DamAdres.Kraj;
                        newKontrahent.Adres.KodKraju = damKontrahent.DamAdres.KodKraju;
                        newKontrahent.Adres.Telefon = damKontrahent.DamAdres.Telefon;
                        newKontrahent.Adres.Faks = damKontrahent.DamAdres.Faks;
                        newKontrahent.Adres.NietypowaLokalizacja = damKontrahent.DamAdres.NietypowaLokalizacja;
                        newKontrahent.PESEL = damKontrahent.PESEL;
                        newKontrahent.REGON = damKontrahent.REGON;
                        newKontrahent.KRS = damKontrahent.KRS;
                        // todo GLN/ILN

                        newKontrahent.Kontakt.EMAIL = damKontrahent.EMAIL;

                        //CECHY
                        newKontrahent.Features["K_NUMER_SOP3"] = damKontrahent.K_NUMER_SOP3;
                        newKontrahent.Features["K_ID_SOP3"] = damKontrahent.K_NUMER_SOP3;
                    }
                    trans.Commit();
                }
                session1.Save();
            }
        }
    }
}