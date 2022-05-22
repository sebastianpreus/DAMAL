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
        public bool JestIncydentalny { get; set; }

        [ForeignKey("DamAdres")]
        public int DamAdresId { get; set; }        
        public DamAdres DamAdres { get; set; }

        //public StatusPodmiotu StatusPodmiotu { get; set; }
        public string StatusPodmiotu { get; set; }
        public bool PodatnikVAT { get; set; }
        public string PESEL { get; set; }
        public string REGON { get; set; }
        public string KRS { get; set; }
        public string KodKraju { get; set; }

        //todo public FormaPrawna FormaPrawna { get; set; }
        public string FormaPrawna { get; set; }

        public string EMAIL { get; set; }
        public bool Blokada { get; set; } //todo nieaktywny, na delete to pole powinno być przestawiane na true
        public StatusNumeruVAT AktualnyStatusVAT { get; set; }

        public DamKontrahent()
        {
            DamAdres = new DamAdres();
        }
    }
}