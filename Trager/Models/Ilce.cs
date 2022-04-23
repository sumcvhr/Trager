using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Trager.Models
{
    public partial class Ilce
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        [ForeignKey("Il")]
        public int IlId { get; set; } 
        public Il Il { get; set; }
        public virtual ICollection <CourierLocation> KuryeKonum{ get; set; }
        
    }
}
