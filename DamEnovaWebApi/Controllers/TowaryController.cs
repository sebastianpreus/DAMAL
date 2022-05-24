using DamEnovaWebApi.Authentication;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using DamEnovaWebApi.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml;

namespace DamEnovaWebApi.Controllers
{
    [BasicAuthentication]
    public class TowaryController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public TowaryController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Dokumenty        
        public IHttpActionResult GetTowary(ODataQueryOptions<DamTowar> queryOptions)
        {
            Filter filter = new Filter();

            if (queryOptions.Filter != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue);
            }

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            TowaryService towaryService = new TowaryService();

            List<DamTowar> towary = towaryService.GetTowary(filter);

            IQueryable result = queryOptions.ApplyTo(towary.AsQueryable());

            return Ok(result, result.GetType());
        }

        // GET: odata/Dokumenty(5)
        public IHttpActionResult GetTowar([FromODataUri] int key, ODataQueryOptions<DamTowar> queryOptions)
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

            Filter filter = new Filter(key);
            if (queryOptions.Filter != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue);
            }

            TowaryService towaryService = new TowaryService();
            List<DamTowar> towary = towaryService.GetTowary(filter);
            IQueryable result = queryOptions.ApplyTo(towary.AsQueryable());
            return Ok(result, result.GetType());
        }

        public IHttpActionResult Post(DamTowar damTowar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TowaryService towaryService = new TowaryService();
                towaryService.PostTowar(damTowar);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "B³¹d podczas tworzenia towaru: " + ex.Message);
            }
            return Created(damTowar);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TowaryService towaryService = new TowaryService();
                towaryService.DeleteBlokadaTowaru(key);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "B³¹d podczas tworzenia kontrahenta: " + ex.Message);
            }
            return Ok();
        }

        private IHttpActionResult Ok(object content, Type type)
        {
            Type resultType = typeof(OkNegotiatedContentResult<>).MakeGenericType(type);
            return Activator.CreateInstance(resultType, content, this) as IHttpActionResult;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                _connection = null;
            }
        }
    }
}
