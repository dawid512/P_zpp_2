using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{
    /// <summary>
    /// Class for creating a grouped shifts object.
    /// </summary>
    public class ShiftGroup
    {
        /// <summary>
        /// Number of shift.
        /// </summary>
        public int ShiftNumber { get; set; }
        /// <summary>
        /// List of all the shifts that are in the one grouped shift.
        /// </summary>
        public List<SingleShift> GroupOfShifts { get; set; }

        public ShiftGroup (int ShiftNumber, List<SingleShift> GroupOfShifts)
        {
            this.ShiftNumber = ShiftNumber;
            this.GroupOfShifts = GroupOfShifts;
        }
    }
}
