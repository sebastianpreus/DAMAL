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
using System.Web.Http.Results;
using DamEnovaWebApi.Authentication;

namespace DamEnovaWebApi.Controllers
{
    [BasicAuthentication]
    public class PrzesunieciaMagazynoweController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;
        // GET: odata/PrzyjeciaMagazynowe

        public PrzesunieciaMagazynoweController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        public IHttpActionResult GetPrzesunieciaMagazynowe(ODataQueryOptions<DamPrzesuniecieMagazynowe> queryOptions)
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

            PrzesunieciaMagazynoweService przesunieciaMagazynoweService = new PrzesunieciaMagazynoweService();

            List<DamPrzesuniecieMagazynowe> przesuniecieMagazynowe = przesunieciaMagazynoweService.GetPrzesuniecieMagazynowe();

            IQueryable result = queryOptions.ApplyTo(przesuniecieMagazynowe.AsQueryable());

            return Ok(result, result.GetType());
        }

        // GET: odata/PrzyjeciaMagazynowe(5)
        public IHttpActionResult GetPrzesuniecieMagazynowe([FromODataUri] int key, ODataQueryOptions<DamPrzyjecieMagazynowe> queryOptions)
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

            // return Ok<DamPrzyjecieMagazynowe>(damPrzyjecieMagazynowe);
            return StatusCode(HttpStatusCode.NotImplemented);
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
