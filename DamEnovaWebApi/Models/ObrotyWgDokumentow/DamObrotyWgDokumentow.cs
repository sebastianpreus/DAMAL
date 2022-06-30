using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamObrotyWgDokumentow : DamModelBase
    {
        public string Typ { get; set; }
        public bool Zatwierdzony { get; set; }
        public bool Korekta { get; set; }
        public DateTime Data { get; set; }
        public string Numer { get; set; }
        public string Kontrahent { get; set; }
        public string KontrahentKod { get; set; }
        public int KontrahentID { get; set; }
        public decimal Marza { get; set; }
        public string MarzaProcent { get; set; }
        public decimal Netto { get; set; }
        public decimal WartoscR { get; set; }
        public decimal WartoscP { get; set; }
        public decimal WartoscRstanUjemny { get; set; }
        public string Waluta { get; set; }

        public virtual ICollection<DamObrotyWgDokumentowPozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamObrotyWgDokumentowPowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamObrotyWgDokumentowZasob> ZasobyDokumentu { get; set; }
    }
}