using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    /// <summary>
    /// Department table of the database.
    /// </summary>
    public class Departures
    {
        /// <summary>
        /// Id of the department.
        /// </summary>
        [Key]
        public int DeprtureId { get; set; }
        /// <summary>
        /// Shift stored in JSON.
        /// </summary>
        public string Shifts { get; set; } //json, json, Json, JSON, JSON!
        /// <summary>
        /// ???
        /// </summary>
        public virtual IEnumerable<ApplicationUser> MyUsers { get; set; }
        /// <summary>
        /// Department name
        /// </summary>
        [Display(Name = "Nazwa działu")]
        public string DepartureName { get; set; }
        /// <summary>
        /// Navigational property for Database.
        /// </summary>
        [Display(Name = "Firma")]
        public virtual Company CompanyID { get; set; }
        /// <summary>
        /// Coordinator id.
        /// </summary>
        [Display(Name = "Koordynator")]
        public virtual ApplicationUser SupervisorId { get; set; }
    }

}