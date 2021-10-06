using DamEnovaWebApi.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamWydanieMagazynowePowiazany : DamWydanieMagazynoweBase
    {
        [ForeignKey("DamWydanieMagazynowe")]
        public int DamWydanieMagazynoweId { get; set; }
        public DamWydanieMagazynowe DamWydanieMagazynowe { get; set; }
    }
}