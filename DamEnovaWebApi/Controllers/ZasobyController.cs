using DamEnovaWebApi.Authentication;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Models;
using DamEnovaWebApi.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace DamEnovaWebApi.Controllers
{
    [BasicAuthentication]
    public class ZasobyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public ZasobyController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Zasobs
        public IHttpActionResult GetZasoby(ODataQueryOptions<DamZasob> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            ZasobyService zasobyService = new ZasobyService();
            List<DamZasob> zasobs = zasobyService.GetZasoby();
            IQueryable result = queryOptions.ApplyTo(zasobs.AsQueryable());
            return Ok(result, result.GetType());
        }

        // GET: odata/Zasobs(5)
        public IHttpActionResult GetDamZasob([FromODataUri] int key, ODataQueryOptions<DamZasob> queryOptions)
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

            ZasobyService zasobyService = new ZasobyService();
            List<DamZasob> zasobs = zasobyService.GetZasoby(key);
            IQueryable result = queryOptions.ApplyTo(zasobs.AsQueryable());
            return Ok(result, result.GetType());
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
