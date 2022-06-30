using DamEnovaWebApi.Models.Base;
using Soneta.CRM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DamEnovaWebApi.Models
{
    public class DamKontrahent : DamModelBase
    {
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        //public string EuVAT { get; set; }
        public string NIP { get; set; }

        [ForeignKey("DamAdres")]
        public int DamAdresId { get; set; }        
        public DamAdres DamAdres { get; set; }

        //public StatusPodmiotu StatusPodmiotu { get; set; }
        public string StatusPodmiotu { get; set; }
        public bool PodatnikVAT { get; set; }
        public string PESEL { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }

        //public string FormaPrawna { get; set; }

        public string EMAIL { get; set; }

        //CECHY
        public string K_NUMER_SOP3 { get; set; }
        public int K_ID_SOP3 { get; set; }


        public DamKontrahent()
        {
            DamAdres = new DamAdres();
        }
    }
}