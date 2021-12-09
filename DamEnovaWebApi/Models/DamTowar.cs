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
    }
}