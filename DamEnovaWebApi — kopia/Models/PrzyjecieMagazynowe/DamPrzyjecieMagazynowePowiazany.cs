using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamPrzyjecieMagazynowePowiazany : DamPrzyjecieMagazynoweBase
    {
        [ForeignKey("DamPrzyjecieMagazynowe")]
        public int DamPrzyjecieMagazynoweId { get; set; }
        public DamPrzyjecieMagazynowe DamPrzyjecieMagazynowe { get; set; }
    }
}