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
    public class KontrahenciController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private Connection _connection;

        public KontrahenciController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        // GET: odata/Kontrahents        
        public IHttpActionResult GetKontrahenci(ODataQueryOptions<DamKontrahent> queryOptions)
        {
            _logger.Info("Start - Pobieranie kontrahentów");
            
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }             

            KontrahenciService kontrahenciService = new KontrahenciService();
            List<DamKontrahent> kontrahents = kontrahenciService.GetKontrahenci();
            IQueryable result = queryOptions.ApplyTo(kontrahents.AsQueryable());
            return Ok(result, result.GetType());            
        }

        // GET: odata/Kontrahents(5)
        public IHttpActionResult GetDamKontrahent([FromODataUri] int key, ODataQueryOptions<DamKontrahent> queryOptions)
        {            
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            KontrahenciService kontrahenciService = new KontrahenciService();
            List<DamKontrahent> kontrahents = kontrahenciService.GetKontrahenci(key);
            IQueryable result = queryOptions.ApplyTo(kontrahents.AsQueryable());
            return Ok(result, result.GetType());
        }

        public IHttpActionResult Post(DamKontrahent damKontrahent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                KontrahenciService kontrahenciService = new KontrahenciService();
                kontrahenciService.PostKontrahent(damKontrahent);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Błąd podczas tworzenia kontrahenta: " + ex.Message);
            }
            return Created(damKontrahent);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                KontrahenciService kontrahenciService = new KontrahenciService();
                kontrahenciService.DeleteBlokadaKontrahenta(key);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Błąd podczas tworzenia kontrahenta: " + ex.Message);
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
