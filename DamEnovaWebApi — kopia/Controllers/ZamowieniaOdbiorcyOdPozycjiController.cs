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
    public class ZamowieniaOdbiorcyOdPozycjiController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;
        // GET: odata/PrzyjeciaMagazynowe

        public ZamowieniaOdbiorcyOdPozycjiController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }

        public IHttpActionResult GetZamowieniaOdbiorcyOdPozycji(ODataQueryOptions<DamZamowienieOdbiorcyOdPozycjiPozycja> queryOptions)
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

            ZamowieniaOdbiorcyOdPozycjiService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyOdPozycjiService();
            List<DamZamowienieOdbiorcyOdPozycjiPozycja> zamowieniaOdbiorcy = zamowieniaOdbiorcyService.GetZamowieniaOdbiorcyOdPozycji(filter);
            IQueryable result = queryOptions.ApplyTo(zamowieniaOdbiorcy.AsQueryable());
            return Ok(result, result.GetType());
        }

        // GET: odata/PrzyjeciaMagazynowe(5)
        public IHttpActionResult GetZamowienieOdbiorcyOdPozycji([FromODataUri] int key, ODataQueryOptions<DamZamowienieOdbiorcyOdPozycjiPozycja> queryOptions)
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

            ZamowieniaOdbiorcyOdPozycjiService zamowieniaOdbiorcyService = new ZamowieniaOdbiorcyOdPozycjiService();
            List<DamZamowienieOdbiorcyOdPozycjiPozycja> zamowieniaOdbiorcy = zamowieniaOdbiorcyService.GetZamowieniaOdbiorcyOdPozycji(filter);
            IQueryable result = queryOptions.ApplyTo(zamowieniaOdbiorcy.AsQueryable());
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
