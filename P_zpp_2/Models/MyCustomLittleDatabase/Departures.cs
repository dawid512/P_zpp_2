﻿using P_zpp_2.Areas.Identity.Data;
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
        public int SupervisorId { get; set; }
        public string Shifts { get; set; } //json, json, Json, JSON, JSON!
        //[ForeignKey("UserId")]
        public int User_id { get; set; } 
    }
}