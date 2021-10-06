using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZamowienieOdbiorcyPozycja : DamModelBase
    {
        public int Lp { get; set; }
        public string Towar { get; set; }
        public double Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public double Cena { get; set; }
        public string Rabat { get; set; }
        public double CenaPoRabacie { get; set; }
        public decimal Wartosc { get; set; }
        public string StavkaVAT { get; set; }
        public int PozostaloIloscWZ { get; set; }
        public int PozostaloIloscFV { get; set; }
        public int StanPokryciaPozycji { get; set; }
        public string Waluta { get; set; }


        [ForeignKey("DamZamowienieOdbiorcy")]
        public int DamZamowienieOdbiorcyId { get; set; }
        public DamZamowienieOdbiorcy DamZamowienieOdbiorcy { get; set; }
    }
}