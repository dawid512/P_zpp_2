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
        public SelectList departure { get; set; }
        public IEnumerable<SelectListItem> Pracownicy { get; set; }

        [Display(Name = "Pracownik")]
        public string Idusera { get; set; }
        [Display(Name = "Placowka")]
        public int Iddepartuers { get; set; }
    }
}
