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
    public class ObrotyWgDokumentowRozchodyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public ObrotyWgDokumentowRozchodyController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        public IHttpActionResult GetObrotyWgDokumentowRozchody(ODataQueryOptions<DamObrotyWgDokumentow> queryOptions)
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

            ObrotyWgDokumentowService obrotyService = new ObrotyWgDokumentowService();
            List<DamObrotyWgDokumentow> obroty = obrotyService.GetObroty("Rozchód");
            IQueryable result = queryOptions.ApplyTo(obroty.AsQueryable());
            return Ok(result, result.GetType());
        }

        // GET: odata/PrzyjeciaMagazynowe(5)
        public IHttpActionResult GetDamObrotyWgDokumentow([FromODataUri] int key, ODataQueryOptions<DamObrotyWgDokumentow> queryOptions)
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

            ObrotyWgDokumentowService obrotyService = new ObrotyWgDokumentowService();
            List<DamObrotyWgDokumentow> obroty = obrotyService.GetObroty("Rozchód", key);
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
