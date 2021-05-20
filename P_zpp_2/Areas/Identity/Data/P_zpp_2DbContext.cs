using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P_zpp_2.Models;

namespace P_zpp_2.Data
{
    public class P_zpp_2DbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Departures> departures { get; set; }
        public DbSet<Messages> messages { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        //public DbSet<AdministrationRole> administrationRoles { get; set; }
        
        
        public P_zpp_2DbContext(DbContextOptions<P_zpp_2DbContext> options)
            : base(options)
        {
        }

        //public P_zpp_2DbContext(DbContextOptions options) : base(options)
        //{

        //}
      


        public P_zpp_2DbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<AdministrationRole>().HasData(
            //    new AdministrationRole { RoleName = "Administrator"},
            //    new AdministrationRole { RoleName = "Koordynator" },
            //    new AdministrationRole { RoleName = "Pracownik" }


            //    );

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
