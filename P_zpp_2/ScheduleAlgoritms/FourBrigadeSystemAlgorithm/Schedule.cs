using CashierAlgorithm.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class Schedule
    {
        public Dictionary<Workers, List<SingleShift>> ScheduleInDictionary { get; set; }
        public List<LastShiftInfo> HangingDays { get; set; }

        public Schedule(Dictionary<Workers, List<SingleShift>> _schedule, List<LastShiftInfo> HangingDays)
        {
            _schedule = ScheduleInDictionary;
            this.HangingDays = HangingDays;
        }
    }
}
