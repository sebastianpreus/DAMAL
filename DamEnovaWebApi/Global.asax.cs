using DamEnovaWebApi.Enova;
using System.Web.Http;

namespace DamEnovaWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);            

            Start.LoadLibraries();
        }
    }
}
