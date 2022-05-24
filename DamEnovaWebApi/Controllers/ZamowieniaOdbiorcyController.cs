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
    public class ZamowieniaOdbiorcyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;
        // GET: odata/PrzyjeciaMagazynowe

        public ZamowieniaOdbiorcyController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        public IHttpActionResult GetZamowieniaOdbiorcy(ODataQueryOptions<DamZamowienieOdbiorcy> queryOptions)
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

            Filter filter = new Filter();
            if (queryOptions.Filter != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue);
            }

            ZamowieniaOdbiorcyService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyService();
            List<DamZamowienieOdbiorcy> zamowieniaOdbiorcy = zamowieniaOdbiorcyService.GetZamowieniaOdbiorcy(filter);
            IQueryable result = queryOptions.ApplyTo(zamowieniaOdbiorcy.AsQueryable());
            return Ok(result, result.GetType());
        }

        // GET: odata/PrzyjeciaMagazynowe(5)
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

            Filter filter = new Filter(key);
            if (queryOptions.Filter != null)
            {
                filter.ParseQuery(queryOptions.Filter.RawValue);
            }

            ZamowieniaOdbiorcyService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyService();
            List<DamZamowienieOdbiorcy> zamowieniaOdbiorcy = zamowieniaOdbiorcyService.GetZamowieniaOdbiorcy(filter);
            IQueryable result = queryOptions.ApplyTo(zamowieniaOdbiorcy.AsQueryable());
            return Ok(result, result.GetType());
        }

        public IHttpActionResult Post(DamZamowienieOdbiorcy damZamowienieOdbiorcy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ZamowieniaOdbiorcyService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyService();
                zamowieniaOdbiorcyService.PostZamowienieOdbiorcy(damZamowienieOdbiorcy);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Zamówienie odbiorcy - błąd podczas tworzenia dokumentu: " + ex.Message); 
            }
            return Created(damZamowienieOdbiorcy);
        }

        public IHttpActionResult Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ZamowieniaOdbiorcyService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyService();
            zamowieniaOdbiorcyService.DeleteZamowienieOdbiorcy(key);

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
