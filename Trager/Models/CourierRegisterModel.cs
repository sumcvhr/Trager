using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trager.Models
{
    public class CourierRegisterModel 
    {
        [MaxLength(50)]
        public string CourierName { get; set; }
        [Required]
        [MaxLength(50)]
        public string CourierLastName { get; set; }
        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string CourierEmail { get; set; }
        [Required]
        [MaxLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string CourierPhone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string CourierPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("CourierPassword")]
        public string CourierRePassword { get; set; }
        public string AvatarAdress { get; set; }

        

        public decimal? GonderimUcreti { get; set; }
    }
}
