using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class EmployementInfo
    {
        [Key]
        public int id { get; set; }
        public bool employmentStatus { get; set; } 
        public bool suspension { get; set; }
        public DateTime employedSince { get; set; }
        public DateTime contratExpirationDate { get; set; }
        public string position { get; set; }
        public string employmentFacility { get; set; }

        public EmployementInfo(bool employmentStatus, bool suspension, DateTime employedSince, DateTime contratExpirationDate, string position, string employmentFacility)
        {
            this.employmentStatus = employmentStatus;
            this.suspension = suspension;
            this.employedSince = employedSince;
            this.contratExpirationDate = contratExpirationDate;
            this.position = position;
            this.employmentFacility = employmentFacility;
        }

        [Obsolete]
        public EmployementInfo()
        {
        }
    }
}
