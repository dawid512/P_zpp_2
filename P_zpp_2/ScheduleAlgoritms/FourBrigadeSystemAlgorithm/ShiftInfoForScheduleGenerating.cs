using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    /// <summary>
    /// Class for creating object that contains instructions for generating algorithm.
    /// </summary>
    public class ShiftInfoForScheduleGenerating
    {
        /// <summary>
        /// Beginning hour of shift.
        /// </summary>
        public DateTime ShiftSetBeginTime { get; set; }
        /// <summary>
        /// Ending hour of shift.
        /// </summary>
        public DateTime ShiftSetEndTime { get; set; }
        /// <summary>
        /// How long should shift be in days.
        /// </summary>
        public int ShiftLengthInDays { get; set; }
        /// <summary>
        /// Is the shift overnight.
        /// </summary>
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
