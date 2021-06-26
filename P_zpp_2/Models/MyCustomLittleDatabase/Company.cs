﻿using P_zpp_2.Data;
using P_zpp_2.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Models.MyCustomLittleDatabase
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public ApplicationUser BossId { get; set; }
        [Display(Name = "Nazwa Firmy")]
        public string CompanyName { get; set; }

    }
}