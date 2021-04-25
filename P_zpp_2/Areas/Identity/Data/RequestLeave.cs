using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Areas.Identity.Data
{
    public class RequestLeave : IdentityRole
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        //public string pracownikid { get; set; }

        [PersonalData]
        [Column(TypeName = "int")]
        public int leaveType { get; set; }

        [PersonalData]
        [Column(TypeName = "DateTime")]
        public DateTime leave_from { get; set; }

        [PersonalData]
        [Column(TypeName = "DateTime")]
        public DateTime leave_untill { get; set; }

        [PersonalData]
        [Column(TypeName = "Bit")]
        public bool requeststatus { get; set; }
    }
}
