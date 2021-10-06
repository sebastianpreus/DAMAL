using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzyjecieMagazynoweZasob : DamModelBase
    {
        public string OkresMagazynowy { get; set; }
        public string Towar { get; set; }
        public string Typ { get; set; }
        public double IloscZasobu { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal Wartosc { get; set; }
        public double Cena { get; set; }

        [ForeignKey("DamPrzyjecieMagazynowe")]
        public int DamPrzyjecieMagazynoweId { get; set; }
        public DamPrzyjecieMagazynowe DamPrzyjecieMagazynowe { get; set; }
    }
}