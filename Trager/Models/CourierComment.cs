using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trager.Models;

namespace Trager.Models
{
    [Table("CourierComment")]
    public class CourierComment
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Courier")]
        public int CourierId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }


        //[ForeignKey("CourierId")]
        public Courier Courier { get; set; }
        //[ForeignKey("UserId")]
        public User User { get; set; }
    }
}
