using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamDokument : DamDokumentBase  
    {
        [Key]
        public int ID { get; set; }
        //[Key]
        public string Numer { get; set; }

        public virtual ICollection<DamDokumentPozycja> PozycjeDokumentu { get; set; }
        public ICollection<DamDokumentPowiazany> DokumentyPowiazane { get; set; }
        public ICollection<DamDokumentZasob> ZasobyDokumentu { get; set; }

        //[ForeignKey("DamDokument")]
        //public string DamDokumentId { get; set; }
        //public DamDokument DamDokument { get; set; }


        //public DamDokument()
        //{
        //    PozycjeDokumentu = new List<DamDokumentPozycja>();
        //    DokumentyPowiazane = new List<DamDokumentPowiazany>();
        //    ZasobyDokumentu = new List<DamDokumentZasob>();
        //}
    }
}