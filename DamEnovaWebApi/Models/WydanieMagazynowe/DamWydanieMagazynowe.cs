using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamWydanieMagazynowe : DamWydanieMagazynoweBase
    {
        public bool Korekta { get; set; }

        public virtual ICollection<DamWydanieMagazynowePozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamWydanieMagazynowePowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamWydanieMagazynoweZasob> ZasobyDokumentu { get; set; }
    }
}