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
    public class WydaniaMagazynoweNaPodstawieZOController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;


        public WydaniaMagazynoweNaPodstawieZOController()
        {
            _connection = new Connection();
            _connection.ConnectToEnova();
        }


        [HttpPost]
        public IHttpActionResult Post(DamWydanieMagazynoweNaPodstawieZO damWydanieMagazynoweNaPodstawieZO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            WydaniaMagazynoweService wydaniaMagazynoweService = new WydaniaMagazynoweService();
            wydaniaMagazynoweService.PostWydaniaMagazynoweNaPodstawieZO(damWydanieMagazynoweNaPodstawieZO);

            // return Created(damPrzyjecieMagazynowe);
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
