using CashierAlgorithm.Database;
using P_zpp_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm

{
    public class CustomScheduleClass
    {
        public Dictionary<ApplicationUser, List<SingleShift>> ScheduleInDictionary { get; set; }
        public List<LastShiftInfo> HangingDays { get; set; }

        public CustomScheduleClass(Dictionary<ApplicationUser, List<SingleShift>> _schedule, List<LastShiftInfo> HangingDays)
        {
            _schedule = ScheduleInDictionary;
            this.HangingDays = HangingDays;
        }
    }
}
