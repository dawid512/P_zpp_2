using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    /// <summary>
    /// Class for creating single shift object.
    /// </summary>
    public class SingleShift
    {
        /// <summary>
        /// Beginning day of shift.
        /// </summary>
        public DateTime ShiftBegin { get; set; }
        /// <summary>
        /// Ending day of shift.
        /// </summary>
        public DateTime ShiftEnd { get; set; }

        public SingleShift(DateTime ShiftBegin, DateTime ShiftEnd)
        {
            this.ShiftBegin = ShiftBegin;
            this.ShiftEnd = ShiftEnd;
        }
    }
}
