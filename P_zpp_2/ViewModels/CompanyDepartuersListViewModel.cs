using Microsoft.AspNetCore.Mvc.Rendering;
using P_zpp_2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ViewModels
{
    public class CompanyDepartuersListViewModel
    {
        public ICollection<Company> company { get; set; }

        public Departures departures { get; set; }
        public IEnumerable<Departures> Departures { get; set; }

        [Display(Name = "Id_firmy")]
        public string idcompany { get; set; }
       
    }
}
