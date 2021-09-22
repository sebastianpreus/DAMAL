using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DamEnovaWebApi.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;

namespace DamEnovaWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<DamKontrahent>("Kontrahenci");
            builder.EntitySet<DamZasob>("Zasoby");
            builder.EntitySet<DamDokument>("Dokumenty");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
        }
    }
}
