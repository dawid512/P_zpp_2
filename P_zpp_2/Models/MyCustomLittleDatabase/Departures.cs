﻿using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Data
{
    public class Departures
    {
        [Key]
        public int DeprtureId { get; set; }
        //[ForeignKey("CompanyID")]
        public Company CompanyID { get; set; }
        //public int DeprtureId { get; set; }
        //[ForeignKey("UserId")]
        public ApplicationUser SupervisorId { get; set; }
        public string Shifts { get; set; } //json, json, Json, JSON, JSON!
        //[ForeignKey("UserId")]

        //[ForeignKey("DeptID")]
        //public virtual ApplicationUser MyDepartures { get; set; }
        public string DepartureName { get; set; }
    }
}