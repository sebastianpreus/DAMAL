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
                    damKontrahent.MapEnovaObject(kontrahent);
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
                        //newKontrahent.Adres.Ulica = damKontrahent.DamAdres.Linia1;
                        //newKontrahent.Adres.NrDomu = damKontrahent.DamAdres.Linia2;
                        //newKontrahent.Adres.NrLokalu = "34";
                        newKontrahent.Adres.Miejscowosc = damKontrahent.DamAdres.Miejscowosc;
                    }
                    trans.Commit();
                }
                session1.Save();
            }
        }
    }
}