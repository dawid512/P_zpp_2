using Microsoft.AspNetCore.Mvc.Rendering;
using P_zpp_2.Data;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ViewModels
{
    public class CompanyDepartuersListViewModel : Departures
    {

        [Display(Name = "Placowka")]
        public ICollection<Departures> departures { get; set; }
        // public IEnumerable<Departures> Departures { get; set; }

        public SelectList company { get; set; }

        [Display(Name = "Firma")]
        public int CompanyID { get; set; }
       
    }
}
