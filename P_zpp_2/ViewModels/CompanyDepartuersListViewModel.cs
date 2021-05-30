using Microsoft.AspNetCore.Mvc.Rendering;
using P_zpp_2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ViewModels
{
    public class CompanyDepartuersListViewModel : Departures
    {
        public SelectList company { get; set; }

        public ICollection<Departures> departures { get; set; }
       // public IEnumerable<Departures> Departures { get; set; }

        [Display(Name = "Firma")]
        public int idcompany { get; set; }
       
    }
}
