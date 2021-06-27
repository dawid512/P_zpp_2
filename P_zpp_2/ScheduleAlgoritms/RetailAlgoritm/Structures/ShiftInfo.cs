namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class ShiftInfo
    {
        public int OpeningShift_BeginningTime { get; set; }
        public int OpeningShift_EndingTime { get; set; }
        public int MorningShift_BeginningTime { get; set; }
        public int MorningShift_EndingTime { get; set; }
        public int RushHourShift_BeginningTime { get; set; }
        public int RushHourShift_EndingTime { get; set; }
        public int EveningShift_BeginningTime { get; set; }
        public int EveningShift_EndingTime { get; set; }
        public int ClosingShift_BeginningTime { get; set; }
        public int ClosingShift_EndingTime { get; set; }
        public int RushShiftRotation_BeginningTime { get; set; }
        public int RushShiftRotation_EndingTime { get; set; }
    }
}