using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;

namespace P_zpp_2.Data
{
    public class P_zpp_2DbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Departures> departures { get; set; }
        public DbSet<Messages> messages { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<Leaves> leaves { get; set; }
        public DbSet<ScheduleInstructions> ScheduleInstructions { get; set; }
        public object Schedule { get; internal set; }

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
            builder.Entity<Departures>()
                .HasMany(x => x.MyUsers)
                .WithOne(x => x.departure)
                .HasForeignKey(x => x.DeptId);
        }
    }
}