using DamEnovaWebApi.Authentication;
using DamEnovaWebApi.Enova;
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

            string dataOd = "";
            string dataDo = "";
            //?$filter=Data gt 2020-01-15 and Data lt 2020-01-18
            if (queryOptions.Filter != null)
            {
                string filter = queryOptions.Filter.RawValue.ToUpper();

                if (filter.Contains("DATA"))
                {
                    if (filter.Contains("GT"))
                        dataOd = filter.Substring(filter.IndexOf("GT") + 3, 10);
                    if (filter.Contains("LT"))
                        dataDo = filter.Substring(filter.IndexOf("LT") + 3, 10);
                }
            }


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

            List<DamDokumentZakupowy> dokumenty = dokumentyService.GetDokumenty(null, dataOd, dataDo);

            IQueryable result = queryOptions.ApplyTo(dokumenty.AsQueryable());

            return Ok(result, result.GetType());
        }        

        // GET: odata/Dokumenty(5)
        public IHttpActionResult GetDamDokumentZakupowy([FromODataUri] int key, ODataQueryOptions<DamDokumentZakupowy> queryOptions)
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

            DokumentyZakupoweService dokumentyService = new DokumentyZakupoweService();
            List<DamDokumentZakupowy> dokumenty = dokumentyService.GetDokumenty(key);
            IQueryable result = queryOptions.ApplyTo(dokumenty.AsQueryable());
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
