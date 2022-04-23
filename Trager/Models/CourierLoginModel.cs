using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class CourierLoginModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        // public string UserName { get; set; }
        public string CourierEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CourierPassword { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
