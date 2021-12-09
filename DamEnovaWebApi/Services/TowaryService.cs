using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using Soneta.Business;
using Soneta.CRM;
using Soneta.Towary;
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

            //
            // Przed rozpoczęciem dodawania nowego obiektu biznesowego 
            // (tj kontrahenta) należy tak jak zwykle utworzyć obiekt sesji
            //
            //using (Session session = Connection.enovalogin.CreateSession(false, false))
            //{

            //    //
            //    // Do kontrahentów wystarczy uzyskać moduł CRM, w którym
            //    // znajduje się odpowiednia kolekcja
            //    //
            //    TowaryModule tm = TowaryModule.GetInstance(session);

            //    //
            //    // Następnie przeba otworzyć transakcje biznesową (nie bazodanową)
            //    // do edycji
            //    //
            //    using (ITransaction trans = session.Logout(true))
            //    {

            //        //
            //        // Tworzymy nowy, pusty obiekt kontrahenta 
            //        //
            //        Towar nt = new Soneta.Towary.Towar();//

            //        //
            //        // Następnie dodajemy pusty obiekt kontrahenta do tabeli. 
            //        //
            //        tm.Towary.AddRow(nt);//			

            //        //
            //        // Inicjujemy wymagane pole kod kontrahenta na przypadkową wartość.
            //        // Pole jest unikalne w bazie danych, wieć jeżeli kontranhent
            //        // o zadanym kodzie już istnienie w bazie danych, to podczas podstawiania
            //        // wartości do property zostanie wygenerowany wyjątek.
            //        //
            //        int nr = new Random().Next(10000);
            //        nt.Nazwa = "Tesotwa nazwa usługi 0001";
            //        nt.Kod = "DAM_0001" + nr;
            //        //nt.Typ = Soneta.Towary.TypTowaru.Usługa;


            //        trans.Commit();
            //    }

            //
            // A na końcu całość zapisujemy do bazy danych
            //
            //    session.Save();
            //}

            using (Session session = Connection.enovalogin.CreateSession(false, false))
            {

                //CRMModule cm = CRMModule.GetInstance(session);
                ////Kontrahenci khlst = cm.Kontrahenci;
                //Kontrahent kh = new Kontrahent();
                //using (ITransaction trans = session.Logout(true))
                //{


                //    //int nr = 5239;//new Random().Next(10000);
                //    // Następnie dodajemy pusty obiekt kontrahenta do tabeli. 
                //    cm.Kontrahenci.AddRow(kh);

                //    //inicjuje kod kontrahenta
                //    //kontrahent.Kod = "?";
                //    kh.Kod = "IK001DAM_A";
                //    kh.Nazwa = "DAMAL testowy 1_A";
                //    kh.Adres.Ulica = "Testowa ulica 1_A";


                //    trans.Commit();

                //}
                //session.Save();


                ///////////////////////////////////
                //TowaryModule tm = TowaryModule.GetInstance(session);//
                //Towar nt = new Soneta.Towary.Towar();//

                //nt.Nazwa = "Tesotwa nazwa usługi 0001";
                //nt.Kod = "DAM_0001";
                //nt.Typ = Soneta.Towary.TypTowaru.Usługa;

                //tm.Towary.AddRow(nt);//			
                ///////////////////////////////////


                TowaryModule tm = TowaryModule.GetInstance(session);
                using (ITransaction trans = session.Logout(true))
                {
                    Towar towar = (Towar)tm.Towary.WgEAN[damTowar.EAN].GetNext();
                    if (towar == null)
                    {
                        Towar newTowar = new Towar();

                        tm.Towary.AddRow(newTowar);

                        newTowar.Kod = damTowar.Kod;
                        newTowar.Nazwa = damTowar.Nazwa;
                        newTowar.EAN = damTowar.EAN;

                    }
                    trans.Commit();
                }
                session.Save();
            }
        }
    }
}