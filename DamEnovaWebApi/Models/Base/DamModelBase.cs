using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Models.Base
{
    public class DamModelBase
    {
        [Key]
        public int ID { get; set; }
    }
}