using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Services
{
    public class ObrotyWgTowarowService
    {

        public List<DamObrotyWgTowarow> GetObroty(Filter filter)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamObrotyWgTowarow> obroty = new List<DamObrotyWgTowarow>();

                HandelModule hamodule = HandelModule.GetInstance(session);
                Magazyn mag = hamodule.Magazyny.Magazyny.WgNazwa["Magazyn główny"];
                TowaryModule tm = TowaryModule.GetInstance(session);
                Towary towary = tm.Towary;
                View view1 = towary.CreateView();

                //if (id != null)
                //    view1.Condition = new FieldCondition.Equal("ID", id);

                ObrotyTowaruWorker o = new ObrotyTowaruWorker();

                int skip = 0;
                foreach (Towar towar in view1)
                {
                    if (skip >= filter.Skip)
                    {

                        DamObrotyWgTowarow damDokument = new DamObrotyWgTowarow();

                        o.Towar = towar;

                        damDokument.ID = towar.ID;

                        damDokument.Typ = towar.Typ.ToString();
                        damDokument.Kod = towar.Kod;
                        damDokument.Nazwa = towar.Nazwa;

                        SetDataFromWorker(damDokument, towar, o);

                        obroty.Add(damDokument);

                        if (filter.Top > 0 && filter.Top == obroty.Count)
                            break;
                    }
                    else
                        skip += 1;
                }
                return obroty;
            }
        }

        public List<DamObrotyWgTowarow> GetObroty(int? id = null)
        {
            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {
                List<DamObrotyWgTowarow> obroty = new List<DamObrotyWgTowarow>();

                HandelModule hamodule = HandelModule.GetInstance(session);
                Magazyn mag = hamodule.Magazyny.Magazyny.WgNazwa["Magazyn główny"];
                TowaryModule tm = TowaryModule.GetInstance(session);
                Towary towary = tm.Towary;
                View view1 = towary.CreateView();

                if (id != null)
                    view1.Condition = new FieldCondition.Equal("ID", id);

                ObrotyTowaruWorker o = new ObrotyTowaruWorker();

                foreach (Towar towar in view1)
                {
                    DamObrotyWgTowarow damDokument = new DamObrotyWgTowarow();

                    o.Towar = towar;

                    damDokument.ID = towar.ID;

                    damDokument.Typ = towar.Typ.ToString();
                    damDokument.Kod = towar.Kod;
                    damDokument.Nazwa = towar.Nazwa;

                    SetDataFromWorker(damDokument, towar, o);

                    obroty.Add(damDokument);
                }
                return obroty;
            }
        }

        private void SetDataFromWorker(DamObrotyWgTowarow damDokument, Towar towar, ObrotyTowaruWorker o)
        {
            try
            {
                damDokument.Ilosc = o.Ilość.Value;
                damDokument.Marza = o.Marża;
                damDokument.MarzaProcent = ((double)o.ProcentMarża);
                damDokument.WartoscP = o.WartośćRozchodu;
                damDokument.WartoscR = o.WartośćRozchodówSprzedaż;
            }
            catch (Exception)
            {
            }
        }
    }
}