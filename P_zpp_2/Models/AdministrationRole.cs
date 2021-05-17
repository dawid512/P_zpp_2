using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Models
{
    public class AdministrationRole : IdentityRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

    }
}
