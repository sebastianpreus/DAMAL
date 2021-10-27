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
    public class MagazynyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public MagazynyController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Dokumenty        
        public IHttpActionResult GetMagazyny(ODataQueryOptions<DamMagazyn> queryOptions)
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

            MagazynyService magazynyService = new MagazynyService();

            List<DamMagazyn> stanyMagazynowe = magazynyService.GetMagazyny();

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
