using Soneta.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamAdres
    {
        public const string KodPL = "PL";
        public const string Polska = "polska";
        public string KodPocztowy { get; set; }
        public string KodKraju { get; set; }
        public string Kraj { get; set; }
        public string Miejscowosc { get; set; }
        public string Pełny { get; set; }
        public string Linia2 { get; set; }
        public string Linia1 { get; set; }
        public string Faks { get; set; }
        public string Telefon { get; set; }

        public DamAdres(Adres adres)
        {
            MapEnovaObject(adres);
        }

        public void MapEnovaObject(Adres adres)
        {
            this.KodPocztowy = adres.KodPocztowyS;
            this.KodKraju = adres.KodKraju;
            this.Kraj = adres.Kraj;
            this.Miejscowosc = adres.Miejscowosc;
            this.Pełny = adres.Pełny;
            this.Linia2 = adres.Linia2;
            this.Linia1 = adres.Linia1;
            this.Faks = adres.Faks;
            this.Telefon = adres.Telefon;
        }

    }
}