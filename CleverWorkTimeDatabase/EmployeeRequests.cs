using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace testyBD.CleverWorkTimeDatabase
{
    public class EmployeeRequests
    {
        [Key]
        public int id { get; set; }
        public int eployeee_id { get; set; }
        public DateTime requestAplicationDate { get; set; }
        public DateTime requestedForDate { get; set; }
        public int requestType { get; set; } // 0-urlop 1-L4 2-macierzynski 3-tacierzynski 4-koronawirus itd
        public string requestReason { get; set; }

        public EmployeeRequests(int eployeee_id, DateTime requestAplicationDate, DateTime requestedForDate, int requestType, string requestReason)
        {
            this.eployeee_id = eployeee_id;
            this.requestAplicationDate = requestAplicationDate;
            this.requestedForDate = requestedForDate;
            this.requestType = requestType;
            this.requestReason = requestReason;
        }
        [Obsolete]
        public EmployeeRequests()
        {
        }
    }
}
