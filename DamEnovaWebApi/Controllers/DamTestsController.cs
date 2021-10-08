using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using DamEnovaWebApi.Models;
using Microsoft.Data.OData;

namespace DamEnovaWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DamEnovaWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DamTest>("DamTests");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DamTestsController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/DamTests
        public IHttpActionResult GetDamTests(ODataQueryOptions<DamTest> queryOptions)
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

            // return Ok<IEnumerable<DamTest>>(damTests);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/DamTests(5)
        public IHttpActionResult GetDamTest([FromODataUri] int key, ODataQueryOptions<DamTest> queryOptions)
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

            // return Ok<DamTest>(damTest);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/DamTests(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<DamTest> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(damTest);

            // TODO: Save the patched entity.

            // return Updated(damTest);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/DamTests
        public IHttpActionResult Post(DamTest damTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(damTest);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/DamTests(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<DamTest> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(damTest);

            // TODO: Save the patched entity.

            // return Updated(damTest);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/DamTests(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
