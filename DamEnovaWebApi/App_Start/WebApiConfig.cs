using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DamEnovaWebApi.Authentication;
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

            builder.EntitySet<DamKontrahent>("Kontrahenci").EntityType.HasOptional(s => s.DamAdres);
            builder.EntitySet<DamZasob>("Zasoby");

            //DokumentyZakupowe
            var damDokument = builder.EntitySet<DamDokumentZakupowy>("DokumentyZakupowe").EntityType;
            damDokument.HasMany(x => x.PozycjeDokumentu);
            damDokument.HasMany(x => x.DokumentyPowiazane);
            damDokument.HasMany(x => x.ZasobyDokumentu);

            //PrzyjeciaMagazynowe
            var damPrzyjeciaMagazynowe = builder.EntitySet<DamPrzyjecieMagazynowe>("PrzyjeciaMagazynowe").EntityType;
            damPrzyjeciaMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damPrzyjeciaMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damPrzyjeciaMagazynowe.HasMany(x => x.ZasobyDokumentu);

            //WydaniaMagazynowe
            var damWydaniaMagazynowe = builder.EntitySet<DamWydanieMagazynowe>("WydaniaMagazynowe").EntityType;
            damWydaniaMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damWydaniaMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damWydaniaMagazynowe.HasMany(x => x.ZasobyDokumentu);

            //DamPrzesuniecieMagazynowe
            var damPrzesuniecieMagazynowe = builder.EntitySet<DamPrzesuniecieMagazynowe>("PrzesunieciaMagazynowe").EntityType;
            damPrzesuniecieMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damPrzesuniecieMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damPrzesuniecieMagazynowe.HasMany(x => x.ZasobyDokumentu);

            //ZamowieniaOdbiorcy
            var damZamowieniaOdbiorcy = builder.EntitySet<DamZamowienieOdbiorcy>("ZamowieniaOdbiorcy").EntityType;
            damZamowieniaOdbiorcy.HasMany(x => x.PozycjeDokumentu);
            damZamowieniaOdbiorcy.HasMany(x => x.DokumentyPowiazane);
            damZamowieniaOdbiorcy.HasMany(x => x.ZasobyDokumentu);

            //Obroty Wg Dokumentów Przychody
            var damObrotyPrzychody = builder.EntitySet<DamObrotyWgDokumentow>("ObrotyWgDokumentowPrzychody").EntityType;
            damObrotyPrzychody.HasMany(x => x.PozycjeDokumentu);
            damObrotyPrzychody.HasMany(x => x.DokumentyPowiazane);
            damObrotyPrzychody.HasMany(x => x.ZasobyDokumentu);

            //Obroty Wg Dokumentów Rozchody
            var damObrotyRozchody = builder.EntitySet<DamObrotyWgDokumentow>("ObrotyWgDokumentowRozchody").EntityType;
            damObrotyRozchody.HasMany(x => x.PozycjeDokumentu);
            damObrotyRozchody.HasMany(x => x.DokumentyPowiazane);
            damObrotyRozchody.HasMany(x => x.ZasobyDokumentu);

            //Obroty Wg Towarów
            var damObrotyTowary = builder.EntitySet<DamObrotyWgTowarow>("ObrotyWgTowarow").EntityType;



            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            config.EnableDependencyInjection();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
