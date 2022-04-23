using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trager.Models;

namespace Trager.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext>options):base(options) 
        {
        }
        public virtual DbSet<Courier> Courier { get; set; }
        public virtual DbSet<Il> Il { get; set; }
        public virtual DbSet<Ilce> Ilce { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<CourierLocation> CourierLocation { get; set; }
        public virtual DbSet<CourierComment> CourierComment { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
    }
}


