using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZamowienieOdbiorcy : DamModelBase
    { 
        public string Typ { get; set; }
        public bool Zatwierdzony { get; set; }
        public bool Anulowany { get; set; }
        public string Potwierdzenie { get; set; }
        public string Numer { get; set; }
        public DateTime Data { get; set; }
        public string Podrzedne { get; set; }
        public string Kontrahent { get; set; }
        public decimal Netto { get; set; }
        public decimal Wartosc { get; set; }
        public string Opis { get; set; }
        public int StanPokrycia { get; set; }
        public bool ZaliczkaPokrywaCalosc { get; set; }
        public string Waluta { get; set; }
        public string Priorytet { get; set; }
        public string Magazyn { get; set; }

        public virtual ICollection<DamZamowienieOdbiorcyPozycja> PozycjeDokumentu { get; set; }
        public virtual ICollection<DamZamowienieOdbiorcyPowiazany> DokumentyPowiazane { get; set; }
        public virtual ICollection<DamZamowienieOdbiorcyZasob> ZasobyDokumentu { get; set; }
    }
}