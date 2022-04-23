using System;
using System.Collections.Generic;

#nullable disable

namespace Trager.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string UserRePassword { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UAvatarAdress { get; set; }
        public ICollection<CourierComment> CourierComment{ get; set; }
    }
}
