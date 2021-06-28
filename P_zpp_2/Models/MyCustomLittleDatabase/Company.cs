using P_zpp_2.Data;
using P_zpp_2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    /// <summary>
    /// Company table for database.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Id of the company.
        /// </summary>
        [Key]
        public int CompanyId { get; set; }
        //[ForeignKey("UserId")]
        /// <summary>
        /// Company boss'es Id.
        /// </summary>
        public ApplicationUser BossId { get; set; }
        [Display(Name = "Nazwa Firmy")]
        ///Name of the company.
        public string CompanyName { get; set; }

    }
}