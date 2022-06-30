using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZamowienieOdbiorcyOdPozycjiZasob : DamModelBase
    {
        public string OkresMagazynowy { get; set; }
        public string Towar { get; set; }
        public string TowarKod { get; set; }
        public int TowarID { get; set; }
        public string Typ { get; set; }
        public double IloscZasobu { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal Wartosc { get; set; }
        public double Cena { get; set; }
        public string DokumentPartia { get; set; }
        public string DokumentPartiaPierwotna { get; set; }



        [ForeignKey("DamZamowienieOdbiorcyOdPozycji")]
        public int DamZamowienieOdbiorcyOdPozycjiId { get; set; }
        public DamZamowienieOdbiorcy DamZamowienieOdbiorcyOdPozycji { get; set; }
    }
}