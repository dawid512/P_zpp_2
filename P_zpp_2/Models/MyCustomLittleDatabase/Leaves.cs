﻿using P_zpp_2.Data;
using System;
using System.Collections.Generic;
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
        public DateTime CheckIn { get; set; }
        [Display(Name = "Data do")]
        public DateTime CheckOut { get; set; }
        [Display(Name = "Nazwa Urlopu")]
        public string Leavesname { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }


        [Display(Name = "Placowka")]
        public  Departures Iddepartuers { get; set; }
        [Display(Name = "Pracownik")]
        public  ApplicationUser Idusera { get; set; }

    }
}