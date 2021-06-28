using System;

namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class ShiftInfo
    {
        public DateTime OpeningShift_BeginningTime { get; set; }
        public DateTime OpeningShift_EndingTime { get; set; }
        public DateTime MorningShift_BeginningTime { get; set; }
        public DateTime MorningShift_EndingTime { get; set; }
        public DateTime RushHourShift_BeginningTime { get; set; }
        public DateTime RushHourShift_EndingTime { get; set; }
        public DateTime EveningShift_BeginningTime { get; set; }
        public DateTime EveningShift_EndingTime { get; set; }
        public DateTime ClosingShift_BeginningTime { get; set; }
        public DateTime ClosingShift_EndingTime { get; set; }
        public DateTime RushShiftRotation_BeginningTime { get; set; }
        public DateTime RushShiftRotation_EndingTime { get; set; }
    }
}