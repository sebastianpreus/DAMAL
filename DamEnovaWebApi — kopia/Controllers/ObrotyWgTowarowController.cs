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
using DamEnovaWebApi.Helpers;

namespace DamEnovaWebApi.Controllers
{
    [BasicAuthentication]
    public class ObrotyWgTowarowController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public ObrotyWgTowarowController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        public IHttpActionResult GetObrotyWgTowarow(ODataQueryOptions<DamObrotyWgTowarow> queryOptions)
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

            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            ObrotyWgTowarowService obrotyService = new ObrotyWgTowarowService();
            List<DamObrotyWgTowarow> obroty = obrotyService.GetObroty(filter);

            IQueryable<DamObrotyWgTowarow> queryable = obroty.AsQueryable();
            var ignoreQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top;
            queryOptions.ApplyTo(queryable, ignoreQueryOptions);
            IQueryable result = queryOptions.ApplyTo(queryable);
            return Ok(result, result.GetType());


            //var ignoreQueryOptions = AllowedQueryOptions.Skip | AllowedQueryOptions.Top;
            //IQueryable result = queryOptions.ApplyTo(obroty.AsQueryable());
            //return Ok(result, result.GetType());
        }

        // GET: odata/PrzyjeciaMagazynowe(5)
        public IHttpActionResult GetDamObrotyWgTowarow([FromODataUri] int key, ODataQueryOptions<DamObrotyWgTowarow> queryOptions)
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

            ObrotyWgTowarowService obrotyService = new ObrotyWgTowarowService();
            List<DamObrotyWgTowarow> obroty = obrotyService.GetObroty(key);
            IQueryable result = queryOptions.ApplyTo(obroty.AsQueryable());
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
