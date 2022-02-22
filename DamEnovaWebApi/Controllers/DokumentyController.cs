using DamEnovaWebApi.Authentication;
using DamEnovaWebApi.Enova;
using DamEnovaWebApi.Helpers;
using DamEnovaWebApi.Models;
using DamEnovaWebApi.Services;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Csdl;
using Microsoft.OData.UriParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;
using System.Xml;


namespace DamEnovaWebApi.Controllers
{
    [BasicAuthentication]
    public class DokumentyController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private Connection _connection;

        public IHttpActionResult Post(DamDokument damDokument)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DokumentyService dokumentyService = new DokumentyService();
            dokumentyService.PostDokument(damDokument);

            return Created(damDokument);
        }


    }
}