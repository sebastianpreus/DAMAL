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
    public class DokumentyZakupoweController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public DokumentyZakupoweController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Dokumenty        
        public IHttpActionResult GetDokumentyZakupowe(ODataQueryOptions<DamDokumentZakupowy> queryOptions)
        {
            //if (queryOptions.Filter == null || queryOptions.Filter.RawValue.Contains("Typ eq"))
            //    return BadRequest("Brak typu dokumentu w zapytaniu");

            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            DokumentyZakupoweService dokumentyService = new DokumentyZakupoweService();

            //ODataUriParser oDataUriParser = new ODataUriParser(DamDokument, new Uri("name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30"));
            //var result = oDataUriParser.ParseFilter();
            //"name eq 'Facebook' or name eq 'Twitter' and subscribers gt 30",
            //edm,
            //typeof(DamDokument));

            List<DamDokumentZakupowy> dokumenty = dokumentyService.GetDokumenty("");

            IQueryable result = queryOptions.ApplyTo(dokumenty.AsQueryable());

            return Ok(result, result.GetType());
        }        

        // GET: odata/Dokumenty(5)
        public IHttpActionResult GetDamDokumentZakupowy([FromODataUri] string key, ODataQueryOptions<DamDokumentZakupowy> queryOptions)
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

            // return Ok<DamDokument>(damDokument);
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
