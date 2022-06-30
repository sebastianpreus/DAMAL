using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models.Base
{
    public class DamZasobBase : DamModelBase
    {
        public string Nazwa { get; set; }
        public double Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal Wartosc { get; set; }
        public double Cena { get; set; }
        public string Dokument { get; set; }
        public string DokumentPartiiPierwotnej { get; set; }
        public DateTime Data { get; set; }
        public string Kontrahent { get; set; }
        public string KontrahentKod { get; set; }
        public int KontrahentID { get; set; }
        public string Typ { get; set; }
        public string TypTowaru { get; set; }
    }
}