using P_zpp_2.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace P_zpp_2.Data
{
    public class WhateverClass
    {

        public int Id { get; set; }
        public string sampletext { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser connector { get; set; } 
    }
}