using DamEnovaWebApi.Authentication;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
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
    public class StanyMagazynoweController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public StanyMagazynoweController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Dokumenty        
        [HttpGet]
        //[DamEnableQueryAttribute]
        //[Route("odata/StanyMagazynowe/{skip}/{top}")]
        //public IHttpActionResult GetStanyMagazynowe(int skip, int top, ODataQueryOptions<DamStanMagazynowy> queryOptions)
        public IHttpActionResult GetStanyMagazynowe(ODataQueryOptions<DamStanMagazynowy> queryOptions)
        {
            Filter filter = new Filter();
            //filter.Skip = skip;
            //filter.Top = top;
            if (queryOptions.Filter != null || queryOptions.Top != null || queryOptions.Skip != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue, //skip, top);
                                  queryOptions.Top?.Value, 
                                  queryOptions.Skip?.Value);
            }

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            StanyMagazynoweService stanyMagazynoweService = new StanyMagazynoweService();

            List<DamStanMagazynowy> stanyMagazynowe = stanyMagazynoweService.GetStanyMagazynowe(filter);

            IQueryable<DamStanMagazynowy> queryable = stanyMagazynowe.AsQueryable();
            var ignoreQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top;
            queryOptions.ApplyTo(queryable, ignoreQueryOptions);
            IQueryable result = queryOptions.ApplyTo(queryable);

            return Ok(result, result.GetType());
        }

        public IHttpActionResult GetStanyMagazynowe(string magazyn, ODataQueryOptions<DamStanMagazynowy> queryOptions)
        {
            Filter filter = new Filter();
            if (queryOptions.Filter != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue);
            }

            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            StanyMagazynoweService stanyMagazynoweService = new StanyMagazynoweService();
            List<DamStanMagazynowy> stanyMagazynowe = stanyMagazynoweService.GetStanyMagazynowe(filter, magazyn);
            IQueryable result = queryOptions.ApplyTo(stanyMagazynowe.AsQueryable());
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
