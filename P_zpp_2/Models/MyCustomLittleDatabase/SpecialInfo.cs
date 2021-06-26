using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class SpecialInfo
    {
        public int? TeamNumber { get; set; }
        public bool? IsWeekendWorker { get; set; }
        public int? GuaranteedFreeDay { get; set; }
        public int? OverTime { get; set; }
        public bool? EagerForOverTime { get; set; }
    }
}
