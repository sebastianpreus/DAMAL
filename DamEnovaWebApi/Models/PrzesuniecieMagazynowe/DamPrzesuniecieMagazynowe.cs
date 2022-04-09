using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzesuniecieMagazynowe : DamModelBase
    {
        public string Typ { get; set; }
        public bool Zatwierdzony { get; set; }
        public string Numer { get; set; }
        public DateTime Data { get; set; }
        public string Magazyn { get; set; }
        public string DoMagazynu { get; set; }
        public decimal Netto { get; set; }
        public decimal VAT { get; set; }
        public decimal Wartosc { get; set; }
        public string Waluta { get; set; }
        public string Priorytet { get; set; }

        public virtual ICollection<DamPrzesuniecieMagazynowePozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamPrzesuniecieMagazynowePowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamPrzesuniecieMagazynoweZasob> ZasobyDokumentu { get; set; }
    }
}