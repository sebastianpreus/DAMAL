using DamEnovaWebApi.Models.Base;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamStanMagazynowy : DamModelBase
    {
        public string NazwaTowaru { get; set; }
        public double StanMagazynu { get; set; }
        public decimal WartoscMagazynu { get; set; }
        public decimal WartoscKsiegowaMagazynu { get; set; }
        public string Kod { get; internal set; }
        
        public Quantity StanRazem { get; internal set; }
        public Quantity StanZamówien { get; internal set; }
        public string EAN { get; internal set; }
    }
}