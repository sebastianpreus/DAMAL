using DamEnovaWebApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DamEnovaWebApi.Models
{
    public class DamDokumentZakupowyPozycja : DamModelBase
    {
        public int Lp { get; set; }
        public string Towar { get; set; }
        public string TowarKod { get; set; }
        public int TowarID { get; set; }
        public double Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public string Zlecenie { get; set; }
        public double Cena { get; set; }
        public decimal Wartosc { get; set; }
        public string StawkaVAT { get; set; }
        public string Waluta { get; set; }

        [ForeignKey("DamDokumentZakupowy")]
        public int DamDokumentId { get; set; }
        public DamDokumentZakupowy DamDokumentZakupowy { get; set; }
    }
}