using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Soneta.Towary;

namespace DamEnovaWebApi.Models
{
    public class DamTowar
    {
        public string Kod { get; set; }
        //[Caption("PKWiU")]
        //[Description("Jest to alias na property SWW")]
        public string PKWiU { get; set; }
        public void MapEnovaObject(Towar towar)
        { 
        
        }
    }
}