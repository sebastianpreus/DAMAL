using DamEnovaWebApi.Models.Base;
using Soneta.Towary;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamStanMagazynowy : DamModelBase
    {

        
        
        public decimal WartoscKsiegowaMagazynu { get; set; }
        
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public double StanZamowien { get; set; }
        public double StanProdukcji { get; set; }
        public double StanZapotrzebowaniaNaSurowce { get; set; }
        public double StanMinus { get; set; }
        public double StanMagazynu { get; set; }
        public decimal WartoscMagazynu { get; set; }
        public double StanKsięgowy { get; set; }
        public decimal WartoscKsiegowa { get; set; }
        public double Hurtowa { get; set; }
        public double SredniaCenaZakupu { get; set; }
        public decimal WartoscHurtowa { get; set; }
        public double Narzut { get; set; }


        public string EAN { get; set; }
        public double StanRazem { get; set; }

        public double Podstawowa { get; set; }
        public double Detaliczna { get; set; }
        public double ProcentVAT { get; set; }
        public double Zarezerwowano { get; set; }
        public double IloscDostepna { get; set; }
        
    }
}