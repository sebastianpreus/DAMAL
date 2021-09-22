using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamDokumentPozycja
    {
        public string Towar { get; set; }
        public double Ilosc { get; set; }
        public string Zlecenie { get; set; }
        public double Cena { get; set; }
        public decimal Wartosc { get; set; }
        public string StawkaVAT { get; set; }

        [ForeignKey("DamDokument")]
        public int DamDokumentId { get; set; }
        public DamDokument DamDokument { get; set; }
    }
}