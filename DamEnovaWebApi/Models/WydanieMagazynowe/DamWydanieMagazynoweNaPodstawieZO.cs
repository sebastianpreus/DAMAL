using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamWydanieMagazynoweNaPodstawieZO : DamModelBase
    {
        public List<int> IdDokumentowZO { get; set; }
    }
}