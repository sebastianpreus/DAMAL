using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamDokument
    {
        public string Symbol { get; internal set; }
        public string KodKontrahenta { get; internal set; }
        public string EAN { get; internal set; }
        public double Ilosc { get; internal set; }
        public decimal Cena { get; internal set; }
    }
}