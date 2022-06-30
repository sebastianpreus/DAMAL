using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZamowienieOdbiorcyOdPozycjiPowiazany : DamModelBase
    {
        public string Typ { get; set; }
        public string Numer { get; set; }
        public bool Zatwierdzony { get; set; }
        public DateTime Data { get; set; }
        public string Kontrahent { get; set; }
        public string KontrahentKod { get; set; }
        public int KontrahentID { get; set; }
        public decimal Netto { get; set; }
        public decimal VAT { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }

        [ForeignKey("DamZamowienieOdbiorcyOdPozycji")]
        public int DamZamowienieOdbiorcyOdPozycjiId { get; set; }
        public DamZamowienieOdbiorcy DamZamowienieOdbiorcyOdPozycji { get; set; }
    }
}