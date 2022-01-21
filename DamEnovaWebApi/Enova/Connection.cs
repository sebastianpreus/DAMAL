using Soneta.Business.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Enova
{
    public class Connection
    {
        public static Login enovalogin;

        public void ConnectToEnova()
        {
            if (enovalogin == null)
            {
                /////////////////////////////////////////////////////////////////
                // Następnie uzyskujemy dostęp do obiektu bazy danych
                // reprezentującego bazę danych jako taką, bez zalogowanego
                // jeszcze operatora. Rejestrowanie bazy danych można zrobić 
                // przy pomocy programu enova. Napis "Demo" jest nazwą bazy
                // wyświetlaną w programie enova.
                //
                //Database database = BusApplication.Instance["Demo"];
                DAMConfig damconf = new DAMConfig();

                //Database database = BusApplication.Instance["24_7 Communication"];
                Database database = BusApplication.Instance[damconf.EnovaFirma];

                ////////////////////////////////////////////////////////////////
                // Kolejnym krokiem jest uzyskanie loginu do bazy danych, czyli
                // zalogowanie się operatora. Oczywiście odpowiedni operator
                // musi być wcześniej wprowadzony w programie enova.
                // W przykładzie poniżej loguję się na operatora "Administrator"
                // z pustym hasłem (brak hasła).
                //
                //enovalogin = database.Login(false, "Damian", "");

                enovalogin = database.Login(false, damconf.EnovaUser, damconf.EnovaUserPwd);
            }

            //Session session = enovalogin.CreateSession(false, false);        
        }
    }
}