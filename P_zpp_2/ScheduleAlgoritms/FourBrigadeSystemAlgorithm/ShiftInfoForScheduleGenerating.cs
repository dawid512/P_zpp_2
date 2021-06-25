using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Database
{
    public class ShiftInfoForScheduleGenerating
    {
        public DateTime ShiftSetBeginTime { get; set; }
        public DateTime ShiftSetEndTime { get; set; }
        public int ScheduleMonth { get; set; }
        public int ShiftLengthInDays { get; set; }
        public int coordinatorId { get; set; }
        public bool IsOvernight { get; set; }

        public ShiftInfoForScheduleGenerating(DateTime ShiftSetBeginTime, DateTime ShiftSetEndTime, int ScheduleMonth, int ShiftLengthInDays, int coordinatorId, bool IsOvernight)
        {
            this.ShiftSetBeginTime = ShiftSetBeginTime;
            this.ShiftSetEndTime = ShiftSetEndTime;
            this.ScheduleMonth = ScheduleMonth;
            this.ShiftLengthInDays = ShiftLengthInDays;
            this.coordinatorId = coordinatorId;
            this.IsOvernight = IsOvernight;
        }
    }
}
