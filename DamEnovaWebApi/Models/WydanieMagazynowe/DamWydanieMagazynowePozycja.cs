using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamWydanieMagazynowePozycja : DamModelBase
    {
        public int Lp { get; set; }
        public string Towar { get; set; }
        public double Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public string Zlecenie { get; set; }
        public double Cena { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }

        [ForeignKey("DamWydanieMagazynowe")]
        public int DamWydanieMagazynoweId { get; set; }
        public DamWydanieMagazynowe DamWydanieMagazynowe { get; set; }
    }
}