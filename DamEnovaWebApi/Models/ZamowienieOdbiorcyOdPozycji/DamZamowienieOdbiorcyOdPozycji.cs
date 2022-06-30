using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZamowienieOdbiorcyOdPozycji : DamModelBase
    {
        public string Typ { get; set; }
        public bool Zatwierdzony { get; set; }
        public bool Anulowany { get; set; }
        public string Numer { get; set; }
        public DateTime Data { get; set; }
        public string Kontrahent { get; set; }
        public string KontrahentKod { get; set; }
        public int KontrahentID { get; set; }
        public decimal Netto { get; set; }
        public decimal Wartosc { get; set; }
        public decimal VAT { get; set; }
        public string Opis { get; set; }
        public string Waluta { get; set; }


        [ForeignKey("DamZamowienieOdbiorcyOdPozycjiPozycja")]
        public int DamZamowienieOdbiorcyOdPozycjiPozycjaId { get; set; }
        public DamZamowienieOdbiorcyOdPozycjiPozycja DamZamowienieOdbiorcyOdPozycjiPozycja { get; set; }


        //public virtual ICollection<DamZamowienieOdbiorcyPozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamZamowienieOdbiorcyOdPozycjiPowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamZamowienieOdbiorcyOdPozycjiZasob> ZasobyDokumentu { get; set; }
    }
}