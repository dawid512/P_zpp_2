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
        [Display(Name = "Firma")]
        public Company companies { get; set; }
        [Display(Name = "Firma")]
        public SelectList company { get; set; }
        [Display(Name = "Firma")]
        public SelectList companyList { get; set; }
        [Display(Name = "Dział")]
        public List<Departures> departures { get; set; }
        [Display(Name = "Dział")]
        public Departures dp { get; set; }
        [Display(Name = "Firma")]
        public int idcompany { get; set; }
       
    }
}
