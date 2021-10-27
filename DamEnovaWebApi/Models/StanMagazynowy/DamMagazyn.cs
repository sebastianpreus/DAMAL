using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamMagazyn : DamModelBase
    {
        public string Nazwa { get; set; }
        public string Symbol { get; set; }
    }
}