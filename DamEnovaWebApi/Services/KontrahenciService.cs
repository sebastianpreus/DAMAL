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
        public List<DamKontrahent> GetKontrahenci()
        {
            //DamalEnova damalEnova = new DamalEnova();
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                CRMModule cm = CRMModule.GetInstance(session);
                Kontrahenci khlst = cm.Kontrahenci;
                Soneta.Business.View khview = khlst.CreateView();

                // I założyć filtr, np tylko kontrahentów, zawierających literkę 
                // 's' w nazwie i o kodzie nie !INCYDENTALNY.
                // Operatory
                // & to jest AND
                // | to jest OR
                // ! to jest NOT

                //khview.Condition &= new FieldCondition.Like("Nazwa", "*s*")
                //    & !new FieldCondition.Equal("Kod", "!INCYDENTALNY");

                //khview.Condition &= new FieldCondition.Like("Nazwa", "*abc*");
                //& new FieldCondition.Like("Kod", "*" + "abc" + "*");

                List<DamKontrahent> kontrahents = new List<DamKontrahent>();

                foreach (Kontrahent kontrahent in khview)
                {
                    DamKontrahent damKontrahent = new DamKontrahent();
                    damKontrahent.MapEnovaObject(kontrahent);
                    kontrahents.Add(damKontrahent);
                }
                return kontrahents;
            }
        }
    }
}