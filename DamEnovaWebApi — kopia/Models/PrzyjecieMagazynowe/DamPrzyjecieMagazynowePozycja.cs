using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzyjecieMagazynowePozycja : DamModelBase
    {
        public int Lp { get; set; }
        public string Towar { get; set; }
        public string TowarEAN { get; set; }
        public string TowarKod { get; set; }
        public double Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public string Zlecenie { get; set; }
        public double Cena { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }

        //CECHY
        public string PDH_TYP_SOP3 { get; set; }
        public int PDH_ID_SOP3 { get; set; }
        public string PDH_NR_SOP3 { get; set; }
        public string PDH_ZP_NrDet_SOP3 { get; set; }
        public string PDH_WZ_SOP3 { get; set; }
        public string PDH_ZO_SOP3 { get; set; }
        public string PDH_ZP_SOP3 { get; set; }


        [ForeignKey("DamPrzyjecieMagazynowe")]
        public int DamPrzyjecieMagazynoweId { get; set; }
        public DamPrzyjecieMagazynowe DamPrzyjecieMagazynowe { get; set; }
    }
}