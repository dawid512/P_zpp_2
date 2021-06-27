using Microsoft.AspNetCore.Mvc.Rendering;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ViewModels
{
    public class LeavesPracownicyListViewModel : Leaves
    {
        public ICollection<Leaves> leaves { get; set; }
        [Display(Name = "Dział" )]
        public ICollection<Departures> departure { get; set; }
        public IEnumerable<SelectListItem> workers { get; set; }

        public Departures singleDep { get; set; }

        [Display(Name = "Pracownik")]
        public string IdUser { get; set; }
        [Display(Name = "Dział")]
        public int IdDepartuers { get; set; }
    }
}
