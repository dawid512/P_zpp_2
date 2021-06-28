using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    /// <summary>
    /// Class for creating info about last shift of schedule. In contains info about shift number, days that are needed to generate in new schedule, team number which was in the shift, and shift type.
    /// </summary>
    public class LastShiftInfo
    {
        public int ShiftNumber { get; set; }
        public int DaysToGenerate { get; set; }
        public int TeamNumber { get; set; }
        public int ShiftType { get; set; }
        /// <summary>
        /// Constructor for initiating LastShiftInfo without TeamNumber.
        /// </summary>
        /// <param name="ShiftNumber"></param>
        /// <param name="DaysToGenerate"></param>
        /// <param name="ShiftType"></param>
        public LastShiftInfo(int ShiftNumber, int DaysToGenerate, int ShiftType)
        {
            this.ShiftNumber = ShiftNumber;
            this.DaysToGenerate = DaysToGenerate;
            this.ShiftType = ShiftType;
        }
        /// <summary>
        /// Constructor for initiating LastShiftInfo with TeamNumber.
        /// </summary>
        /// <param name="ShiftNumber"></param>
        /// <param name="DaysToGenerate"></param>
        /// <param name="TeamNumber"></param>
        /// <param name="ShiftType"></param>
        public LastShiftInfo(int ShiftNumber, int DaysToGenerate, int TeamNumber, int ShiftType)
        {
            this.ShiftNumber = ShiftNumber;
            this.DaysToGenerate = DaysToGenerate;
            this.TeamNumber = TeamNumber;
            this.ShiftType = ShiftType;
        }
    }
}
