using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    public class ShiftInfoForScheduleGenerating
    {
        public DateTime ShiftSetBeginTime { get; set; }
        public DateTime ShiftSetEndTime { get; set; }
        public int ShiftLengthInDays { get; set; }
        public bool IsOvernight { get; set; }

        public ShiftInfoForScheduleGenerating(DateTime ShiftSetBeginTime, DateTime ShiftSetEndTime, int ShiftLengthInDays, bool IsOvernight)
        {
            this.ShiftSetBeginTime = ShiftSetBeginTime;
            this.ShiftSetEndTime = ShiftSetEndTime;
            this.ShiftLengthInDays = ShiftLengthInDays;
            this.IsOvernight = IsOvernight;
        }

        public ShiftInfoForScheduleGenerating()
        {
        }
    }
}
