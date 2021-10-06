using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamObrotyWgTowarow : DamModelBase
    {
        public string Typ { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public double Ilosc { get; set; }
        public decimal Marza { get; set; }
        public double MarzaProcent { get; set; }
        public decimal WartoscP { get; set; }
        public decimal WartoscR { get; set; }
    }
}