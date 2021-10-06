using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Authentication
{
    public class UserValidate
    {
        public static bool Login(string username, string password)
        {
            return username.Equals("damal", StringComparison.OrdinalIgnoreCase) && password == "Lamad1#";
        }
    }
}