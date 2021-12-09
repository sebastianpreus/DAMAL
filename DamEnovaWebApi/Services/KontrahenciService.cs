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
    }
}