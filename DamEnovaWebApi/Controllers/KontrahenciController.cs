using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using DamEnovaWebApi.Models;
using Microsoft.OData;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Services;

namespace DamEnovaWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DamEnovaWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DamKontrahent>("Kontrahents");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class KontrahenciController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        // GET: odata/Kontrahents
        [EnableQuery]
        public IHttpActionResult GetKontrahenci(ODataQueryOptions<DamKontrahent> queryOptions)
        {
            _logger.Info("Start - Pobieranie kontrahentów");
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

            KontrahenciService kontrahenciService = new KontrahenciService();
            List<DamKontrahent> kontrahents = kontrahenciService.GetKontrahenci();

            IQueryable results = queryOptions.ApplyTo(kontrahents.AsQueryable(), new ODataQuerySettings());

            return Ok<IEnumerable<DamKontrahent>>((IQueryable<DamKontrahent>)results);

            // return Ok<IEnumerable<DamKontrahent>>(damKontrahents);
            //return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/Kontrahents(5)
        public IHttpActionResult GetDamKontrahent([FromODataUri] string key, ODataQueryOptions<DamKontrahent> queryOptions)
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

            // return Ok<DamKontrahent>(damKontrahent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Kontrahents(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<DamKontrahent> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(damKontrahent);

            // TODO: Save the patched entity.

            // return Updated(damKontrahent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Kontrahents
        public IHttpActionResult Post(DamKontrahent damKontrahent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(damKontrahent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Kontrahents(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<DamKontrahent> delta)
        {
            Validate(delta.GetInstance());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(damKontrahent);

            // TODO: Save the patched entity.

            // return Updated(damKontrahent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Kontrahents(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
