using DamEnovaWebApi.Models.Base;
using Soneta.Handel;
using Soneta.Magazyny;
using Soneta.Towary;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models
{
    public class DamZasob : DamZasobBase
    {
        [Key]
        public string Kod { get; set; }

    }
}