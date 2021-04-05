using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = CleverWorkTimeDatabase;") { }

        public DbSet<EmployementInfo> employementInfo { get; set; }
        public DbSet<PersonalInfo> personalInfo { get; set; }
        public DbSet<PersonalProfileInfo> personalProfileInfo { get; set; }
        public DbSet<ScheduleHistory> legacyScheduleHistory { get; set; }
        public DbSet<Schedule> currentWeekSchedule { get; set; }
        public DbSet<Schedule> nextWeekSchedule { get; set; }
        public DbSet<EmployeeRequests> employeeRequests { get; set; }
        public DbSet<EmployeeRequests> legacyEmployeeRequests { get; set; }
    }
}
