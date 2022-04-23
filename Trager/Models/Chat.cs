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
    [Table("Chat")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("Courier")]
        public int CourierId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }


        public Courier Courier { get; set; }
        public User User { get; set; }
    }
}

