using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{

    [Table("CourierToken")]
    public class CourierToken
    {
        [Key]
        public int Id { get; set; }
        public int Token { get; set; }
        [ForeignKey("Courier")]
        public int CourierId { get; set; }
        public Courier Courier { get; set; }
    }
  
}
