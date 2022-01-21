using DamEnovaWebApi.Enova;
using System.Web.Http;

namespace DamEnovaWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Start.LoadLibraries();

            GlobalConfiguration.Configure(WebApiConfig.Register);            
        }
    }
}
