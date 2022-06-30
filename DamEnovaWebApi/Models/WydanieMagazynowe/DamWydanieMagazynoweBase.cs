using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamWydanieMagazynoweBase : DamModelBase
    {
        public string Typ { get; set; }
        public bool Zatwierdzony { get;  set; }
        public string Numer { get; set; }
        public DateTime Data { get; set; }
        public DateTime DataOperacji { get; set; }
        public string Magazyn { get; set; }
        public string Kontrahent { get; set; }
        public string KontrahentKod { get; set; }
        public int KontrahentID { get; set; }
        public decimal Netto { get; set; }
        public decimal VAT { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }
        public string Opis { get; set; }

        //CECHY
        public string DH_TYP_SOP3 { get; set; }
        public int DH_ID_SOP3 { get; set; }
        public string DH_NR_SOP3 { get; set; }

    }
}