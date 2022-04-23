using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    [Table("CourierLocation")]
    public class CourierLocation
    { 
        [Key]
        public int Id { get; set; }
        [ForeignKey("Courier")]
        public int CourierId { get; set; }
        [ForeignKey("Ilce")] 
        public int IlceId { get; set; }

        [ForeignKey("CourierId")]
        public virtual Courier Courier { get; set; }

        [ForeignKey("IlceId")]
        public virtual Ilce Ilce { get; set; }
    }
}
