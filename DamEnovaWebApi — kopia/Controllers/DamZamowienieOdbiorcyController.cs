using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
//using System.Web.Http.OData;
//using System.Web.Http.OData.Query;
//using System.Web.Http.OData.Routing;
using DamEnovaWebApi.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
//using Microsoft.Data.OData;
using Microsoft.OData;

namespace DamEnovaWebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using DamEnovaWebApi.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<DamZamowienieOdbiorcy>("DamZamowienieOdbiorcy");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class DamZamowienieOdbiorcyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();

        // GET: odata/DamZamowienieOdbiorcy
        public IHttpActionResult GetDamZamowienieOdbiorcy(ODataQueryOptions<DamZamowienieOdbiorcy> queryOptions)
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

            // return Ok<IEnumerable<DamZamowienieOdbiorcy>>(damZamowienieOdbiorcies);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // GET: odata/DamZamowienieOdbiorcy(5)
        public IHttpActionResult GetDamZamowienieOdbiorcy([FromODataUri] int key, ODataQueryOptions<DamZamowienieOdbiorcy> queryOptions)
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

            // return Ok<DamZamowienieOdbiorcy>(damZamowienieOdbiorcy);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/DamZamowienieOdbiorcy(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<DamZamowienieOdbiorcy> delta)
        {
            //Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(damZamowienieOdbiorcy);

            // TODO: Save the patched entity.

            // return Updated(damZamowienieOdbiorcy);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/DamZamowienieOdbiorcy
        public IHttpActionResult Post(DamZamowienieOdbiorcy damZamowienieOdbiorcy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(damZamowienieOdbiorcy);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/DamZamowienieOdbiorcy(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<DamZamowienieOdbiorcy> delta)
        {
            //Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(damZamowienieOdbiorcy);

            // TODO: Save the patched entity.

            // return Updated(damZamowienieOdbiorcy);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/DamZamowienieOdbiorcy(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
