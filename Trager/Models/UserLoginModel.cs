using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class UserLoginModel 
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        // public string UserName { get; set; }
        public string UserEmail { get; set; }

        [Required]
     
        public string UserPassword { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
