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

            builder.EntitySet<DamDokumentZakupowyPozycja>("DamDokumentZakupowyPozycja").EntityType.HasOptional(s => s.DamDokumentZakupowy);
            builder.EntitySet<DamDokumentZakupowyPowiazany>("DamDokumentZakupowyPowiazany").EntityType.HasOptional(s => s.DamDokumentZakupowy);
            builder.EntitySet<DamDokumentZakupowyZasob>("DamDokumentZakupowyZasob").EntityType.HasOptional(s => s.DamDokumentZakupowy);

            //PrzyjeciaMagazynowe
            var damPrzyjeciaMagazynowe = builder.EntitySet<DamPrzyjecieMagazynowe>("PrzyjeciaMagazynowe").EntityType;
            damPrzyjeciaMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damPrzyjeciaMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damPrzyjeciaMagazynowe.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamPrzyjecieMagazynowePozycja>("DamPrzyjecieMagazynowePozycja").EntityType.HasOptional(s => s.DamPrzyjecieMagazynowe);
            builder.EntitySet<DamPrzyjecieMagazynowePowiazany>("DamPrzyjecieMagazynowePowiazany").EntityType.HasOptional(s => s.DamPrzyjecieMagazynowe);
            builder.EntitySet<DamPrzyjecieMagazynoweZasob>("DamPrzyjecieMagazynoweZasob").EntityType.HasOptional(s => s.DamPrzyjecieMagazynowe);

            //WydaniaMagazynowe
            var damWydaniaMagazynowe = builder.EntitySet<DamWydanieMagazynowe>("WydaniaMagazynowe").EntityType;
            damWydaniaMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damWydaniaMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damWydaniaMagazynowe.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamWydanieMagazynowePozycja>("DamWydanieMagazynowePozycja").EntityType.HasOptional(s => s.DamWydanieMagazynowe);
            builder.EntitySet<DamWydanieMagazynowePowiazany>("DamWydanieMagazynowePowiazany").EntityType.HasOptional(s => s.DamWydanieMagazynowe);
            builder.EntitySet<DamWydanieMagazynoweZasob>("DamWydanieMagazynoweZasob").EntityType.HasOptional(s => s.DamWydanieMagazynowe);

            //DamPrzesuniecieMagazynowe
            var damPrzesuniecieMagazynowe = builder.EntitySet<DamPrzesuniecieMagazynowe>("PrzesunieciaMagazynowe").EntityType;
            damPrzesuniecieMagazynowe.HasMany(x => x.PozycjeDokumentu);
            damPrzesuniecieMagazynowe.HasMany(x => x.DokumentyPowiazane);
            damPrzesuniecieMagazynowe.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamPrzesuniecieMagazynowePozycja>("DamPrzesuniecieMagazynowePozycja").EntityType.HasOptional(s => s.DamPrzesuniecieMagazynowe);
            builder.EntitySet<DamPrzesuniecieMagazynowePowiazany>("DamPrzesuniecieMagazynowePowiazany").EntityType.HasOptional(s => s.DamPrzesuniecieMagazynowe);
            builder.EntitySet<DamPrzesuniecieMagazynoweZasob>("DamPrzesuniecieMagazynoweZasob").EntityType.HasOptional(s => s.DamPrzesuniecieMagazynowe);

            //ZamowieniaOdbiorcy
            var damZamowieniaOdbiorcy = builder.EntitySet<DamZamowienieOdbiorcy>("ZamowieniaOdbiorcy").EntityType;
            damZamowieniaOdbiorcy.HasMany(x => x.PozycjeDokumentu);
            damZamowieniaOdbiorcy.HasMany(x => x.DokumentyPowiazane);
            damZamowieniaOdbiorcy.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamZamowienieOdbiorcyPozycja>("DamZamowienieOdbiorcyPozycja").EntityType.HasOptional(s => s.DamZamowienieOdbiorcy);
            builder.EntitySet<DamZamowienieOdbiorcyPowiazany>("DamZamowienieOdbiorcyPowiazany").EntityType.HasOptional(s => s.DamZamowienieOdbiorcy);
            builder.EntitySet<DamZamowienieOdbiorcyZasob>("DamZamowienieOdbiorcyZasob").EntityType.HasOptional(s => s.DamZamowienieOdbiorcy);

            //Obroty Wg Dokumentów Przychody
            var damObrotyPrzychody = builder.EntitySet<DamObrotyWgDokumentow>("ObrotyWgDokumentowPrzychody").EntityType;
            damObrotyPrzychody.HasMany(x => x.PozycjeDokumentu);
            damObrotyPrzychody.HasMany(x => x.DokumentyPowiazane);
            damObrotyPrzychody.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamObrotyWgDokumentowPozycja>("DamObrotyWgDokumentowPozycja").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);
            builder.EntitySet<DamObrotyWgDokumentowPowiazany>("DamObrotyWgDokumentowPowiazany").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);
            builder.EntitySet<DamObrotyWgDokumentowZasob>("DamObrotyWgDokumentowZasob").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);

            //Obroty Wg Dokumentów Rozchody
            var damObrotyRozchody = builder.EntitySet<DamObrotyWgDokumentow>("ObrotyWgDokumentowRozchody").EntityType;
            damObrotyRozchody.HasMany(x => x.PozycjeDokumentu);
            damObrotyRozchody.HasMany(x => x.DokumentyPowiazane);
            damObrotyRozchody.HasMany(x => x.ZasobyDokumentu);

            builder.EntitySet<DamObrotyWgDokumentowPozycja>("DamObrotyWgDokumentowPozycja").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);
            builder.EntitySet<DamObrotyWgDokumentowPowiazany>("DamObrotyWgDokumentowPowiazany").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);
            builder.EntitySet<DamObrotyWgDokumentowZasob>("DamObrotyWgDokumentowZasob").EntityType.HasOptional(s => s.DamObrotyWgDokumentow);


            //ZamowieniaOdbiorcyOdPozycji
            var damZamowieniaOdbiorcyOdPozycjiPozycja = builder.EntitySet<DamZamowienieOdbiorcyOdPozycjiPozycja>("ZamowieniaOdbiorcyOdPozycji").EntityType;
            damZamowieniaOdbiorcyOdPozycjiPozycja.HasRequired(x => x.DamZamowienieOdbiorcyOdPozycji);
            
            //damZamowieniaOdbiorcyOdPozycji.HasMany(x => x.DokumentyPowiazane);
            //damZamowieniaOdbiorcyOdPozycji.HasMany(x => x.ZasobyDokumentu);

            //var damZamowienieOdbiorcyOdPozycji = builder.EntitySet<DamZamowienieOdbiorcyOdPozycji>("DamZamowienieOdbiorcyOdPozycji").EntityType.HasOptional(s => s.DamZamowienieOdbiorcyOdPozycjiPozycja);

            builder.EntitySet<DamZamowienieOdbiorcyOdPozycjiPowiazany>("DamZamowienieOdbiorcyOdPozycjiPowiazany").EntityType.HasOptional(s => s.DamZamowienieOdbiorcyOdPozycji);
            builder.EntitySet<DamZamowienieOdbiorcyOdPozycjiZasob>("DamZamowienieOdbiorcyOdPozycjiZasob").EntityType.HasOptional(s => s.DamZamowienieOdbiorcyOdPozycji);


            //Obroty Wg Towarów
            var damObrotyTowary = builder.EntitySet<DamObrotyWgTowarow>("ObrotyWgTowarow").EntityType;

            //Stany Magazynowe
            var damStanyMagazynowe = builder.EntitySet<DamStanMagazynowy>("StanyMagazynowe").EntityType;

            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            config.EnableDependencyInjection();

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}
