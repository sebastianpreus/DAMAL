using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzesuniecieMagazynoweZasob : DamModelBase
    {
        public string OkresMagazynowy { get; set; }
        public string Towar { get; set; }
        public string Typ { get; set; }
        public double IloscZasobu { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal Wartosc { get; set; }
        public double Cena { get; set; }

        [ForeignKey("DamPrzesuniecieMagazynowe")]
        public int DamPrzesuniecieMagazynoweId { get; set; }
        public DamPrzesuniecieMagazynowe DamPrzesuniecieMagazynowe { get; set; }
    }
}