using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamDokumentZakupowyBase : DamModelBase
    {
        public string Typ { get; set; }
        public bool Zatwierdzony { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataOperacji { get; set; }
        public string Kontrahent { get; set; }
        public decimal Netto { get; set; }
        public decimal VAT { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }
    }
}