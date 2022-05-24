using DamEnovaWebApi.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DamEnovaWebApi.Models
{
    public class DamDokumentZakupowy : DamDokumentZakupowyBase  
    {
        public string Numer { get; set; }
        public bool Korekta { get; set; }

        public virtual ICollection<DamDokumentZakupowyPozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamDokumentZakupowyPowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamDokumentZakupowyZasob> ZasobyDokumentu { get; set; }
    }
}