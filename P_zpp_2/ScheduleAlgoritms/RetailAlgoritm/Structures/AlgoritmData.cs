using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class AlgoritmData
    {
        public AlgoritmSettings algoritmSettings { get; set; }
        public List<RetailWorker> retailWorkerList { get; set; }
        public ShiftInfo shiftInfo { get; set; }
    }
}
