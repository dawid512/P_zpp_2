using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class RetailWorker
    {
        public string WorkerId { get; set; }
        public string WorkerName { get; set; }
        public int WorkerLastShift { get; set; }
        public List<DateTime> OffWorkDays { get; set; }
    }
}
