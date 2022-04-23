using System;
using System.Collections.Generic;

#nullable disable

namespace Trager.Models
{
    public partial class Courier
    {
        public int Id { get; set; }
        public string CourierName { get; set; }
        public string CourierLastName { get; set; }
        public string CourierEmail { get; set; }
        public string CourierPhone { get; set; }
        public string CourierPassword { get; set; }
        public string CourierRePassword { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; } 
        public string AvatarAdress { get; set; }
        public decimal? GonderimUcreti { get; set; }
        public ICollection<CourierComment> CourierComment { get; set; }
    }
}
