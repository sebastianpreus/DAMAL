using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using DamEnovaWebApi.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace DamEnovaWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DamEnovaWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DamDokument>("Dokumenty");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DokumentyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();


        // GET: odata/Dokumenty
        [EnableQueryAttribute]
        public IQueryable<DamDokument> GetDokumenty(ODataQueryOptions<DamDokument> queryOptions)
        {
            //if (queryOptions.Filter == null || queryOptions.Filter.RawValue.Contains("Typ eq"))
            //    return BadRequest("Brak typu dokumentu w zapytaniu");

            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                //return BadRequest(ex.Message);
            }

            Connection connection = new Connection();
            connection.ConnectToEnova();

            DokumentyService dokumentyService = new DokumentyService();

            //ODataUriParser oDataUriParser = new ODataUriParser(DamDokument, new Uri("name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30"));
            //var result = oDataUriParser.ParseFilter();
            //"name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30",
            //edm,
            //typeof(DamDokument));

            List<DamDokument> dokumenty = dokumentyService.GetDokumenty("");

            IQueryable results = queryOptions.ApplyTo(dokumenty.AsQueryable(), new ODataQuerySettings());
            return (IQueryable<DamDokument>)results;// Ok<IEnumerable<DamDokument>>((IQueryable<DamDokument>)results);
        }

        //      // GET: odata/Dokumenty
        //      [Queryable]
        //      public IHttpActionResult GetDokumenty(ODataQueryOptions<DamDokument> queryOptions)
        //      {
        //          //if (queryOptions.Filter == null || queryOptions.Filter.RawValue.Contains("Typ eq"))
        //          //    return BadRequest("Brak typu dokumentu w zapytaniu");

        //          // validate the query.
        //          try
        //          {
        //              queryOptions.Validate(_validationSettings);
        //          }
        //          catch (ODataException ex)
        //          {
        //              return BadRequest(ex.Message);
        //          }

        //          Connection connection = new Connection();
        //          connection.ConnectToEnova();

        //          DokumentyService dokumentyService = new DokumentyService();

        //          //ODataUriParser oDataUriParser = new ODataUriParser(DamDokument, new Uri("name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30"));
        //          //var result = oDataUriParser.ParseFilter();
        ////"name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30",
        ////edm,
        ////typeof(DamDokument));

        //          List<DamDokument> dokumenty = dokumentyService.GetDokumenty("");

        //          IQueryable results = queryOptions.ApplyTo(dokumenty.AsQueryable(), new ODataQuerySettings());
        //          return Ok<IEnumerable<DamDokument>>((IQueryable<DamDokument>)results);
        //      }

        // GET: odata/Dokumenty(5)
        public IHttpActionResult GetDamDokument([FromODataUri] string key, ODataQueryOptions<DamDokument> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<DamDokument>(damDokument);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Dokumenty(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<DamDokument> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(damDokument);

            // TODO: Save the patched entity.

            // return Updated(damDokument);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Dokumenty
        public IHttpActionResult Post(DamDokument damDokument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(damDokument);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Dokumenty(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<DamDokument> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(damDokument);

            // TODO: Save the patched entity.

            // return Updated(damDokument);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Dokumenty(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
