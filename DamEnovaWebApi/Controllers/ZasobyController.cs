using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using DamEnovaWebApi.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;

namespace DamEnovaWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DamEnovaWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DamZasob>("Zasobs");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ZasobyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/Zasobs
        public IHttpActionResult GetZasoby(ODataQueryOptions<DamZasob> queryOptions)
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

            Connection connection = new Connection();
            connection.ConnectToEnova();

            ZasobyService zasobyService = new ZasobyService();
            List<DamZasob> zasobs = zasobyService.GetZasoby();

            IQueryable results = queryOptions.ApplyTo(zasobs.AsQueryable(), new ODataQuerySettings());
            return Ok<IEnumerable<DamZasob>>((IQueryable<DamZasob>)results);

            // return Ok<IEnumerable<DamZasob>>(damZasobs);
            //return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/Zasobs(5)
        public IHttpActionResult GetDamZasob([FromODataUri] string key, ODataQueryOptions<DamZasob> queryOptions)
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

            // return Ok<DamZasob>(damZasob);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Zasobs(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<DamZasob> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(damZasob);

            // TODO: Save the patched entity.

            // return Updated(damZasob);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Zasobs
        public IHttpActionResult Post(DamZasob damZasob)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(damZasob);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Zasobs(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<DamZasob> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(damZasob);

            // TODO: Save the patched entity.

            // return Updated(damZasob);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Zasobs(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
