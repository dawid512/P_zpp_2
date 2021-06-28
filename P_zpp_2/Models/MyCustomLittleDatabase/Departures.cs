using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    public class Departures
    {
        [Key]
        public int DeprtureId { get; set; }
        public string Shifts { get; set; } //json, json, Json, JSON, JSON!
        public virtual IEnumerable<ApplicationUser> MyUsers { get; set; }
        [Display(Name = "Nazwa działu")]
        public string DepartureName { get; set; }



        [Display(Name = "Firma")]
        public virtual Company CompanyID { get; set; }
        [Display(Name = "Koordynator")]
        public virtual ApplicationUser SupervisorId { get; set; }
    }

}