using DamEnovaWebApi.Models.Base;
using Soneta.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamAdres : DamModelBase
    {
        public const string KodPL = "PL";
        public const string Polska = "polska";
        public int KodPocztowy { get; set; }
        public string Kraj { get; set; }
        public string Miejscowosc { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        public string Faks { get; set; }
        public string Telefon { get; set; }

        public string Poczta { get; set; }
        public string Gmina { get; set; }
        public string Powiat { get; set; }
        public string Wojewodztwo { get; set; }
        public string KodKraju { get; set; }
        public string NietypowaLokalizacja { get; set; }

        public virtual ICollection<DamKontrahent> DamKontrahenci { get; set; }
    }
}