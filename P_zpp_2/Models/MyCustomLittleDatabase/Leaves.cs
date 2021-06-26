using P_zpp_2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    public class Leaves
    {
        

        [Key]
        public int Id { get; set; }
        [Display(Name = "Data od")]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; } = DateTime.Now;

        [Display(Name = "Data do")]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; } = DateTime.Now;

        [Display(Name = "Nazwa Urlopu")]
        public string Leavesname { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        [Display(Name = "Status Zaakceptowane")]
        public bool Status_zaakceptopwane { get; set; }
        [Display(Name = "Status Odrzucone")]
        public bool Status_odrzucone { get; set; }
        public string LeaveDayRange { get; set; }


        [Display(Name = "Dział")]
        public virtual  Departures Iddepartuers { get; set; }
        [Display(Name = "Pracownik")]
        public virtual ApplicationUser Idusera { get; set; }

    }
}
