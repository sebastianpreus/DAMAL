using DamEnovaWebApi.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DamEnovaWebApi.Models
{
    public class DamDokumentZakupowyZasob : DamZasobBase
    {
        public string Kod { get; set; }

        [ForeignKey("DamDokumentZakupowy")]
        public int DamDokumentId { get; set; }
        public DamDokumentZakupowy DamDokumentZakupowy { get; set; }
    }
}