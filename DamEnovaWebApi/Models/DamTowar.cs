using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamTowar : DamModelBase
    {
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string EAN { get; set; }
        public string NumerKatalogowy { get; set; }
        public string Jednostka { get; set; }
        public string PKWiU { get; set; }

        //CECHY
        public string T_TYP_SOP3 { get; set; }
        public int T_ID_SOP3 { get; set; }
        public string T_NR_SOP3 { get; set; }
        public string T_Nr_rys { get; set; }
        public string T_Material_wyjsc { get; set; }
        public int T_SAP { get; set; }
        public string T_Poz_kat_Bamet { get; set; }
        public string T_Uwagi { get; set; }
        public string T_Uwagi_wew { get; set; }
        public string T_Kontrolka { get; set; }
        public string T_Cecha { get; set; }
        public string T_Rodzaj_Kategoria { get; set; }
        public string T_Gatunek_Prod { get; set; }
        public string T_Grupa { get; set; }
        public string T_Rodzina { get; set; }
    }
}