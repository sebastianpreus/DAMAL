using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzyjecieMagazynowe : DamPrzyjecieMagazynoweBase
    {
        public bool Korekta { get; set; }
        public virtual ICollection<DamPrzyjecieMagazynowePozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamPrzyjecieMagazynowePowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamPrzyjecieMagazynoweZasob> ZasobyDokumentu { get; set; }
    }
}