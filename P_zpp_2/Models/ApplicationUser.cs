using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using P_zpp_2.Data;
using P_zpp_2.Models.MyCustomLittleDatabase;

namespace P_zpp_2.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Rola { get; set; }
        [PersonalData]
        [Column(TypeName = "nvarchar(900)")]
        public string Schedule { get; set; }

        public int? DeptId { get; set; }
        public virtual Departures departure { get; set; }

    }
}
