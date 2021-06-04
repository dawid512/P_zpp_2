using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ViewModels
{
    public class ForDeparturesViewModel
    {
        public Departures departures { get; set; }
        public List<ApplicationUser> appUsers { get; set; }
    }
}
