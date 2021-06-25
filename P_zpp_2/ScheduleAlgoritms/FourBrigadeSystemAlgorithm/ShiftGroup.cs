using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class ShiftGroup
    {
        public int ShiftNumber { get; set; }
        public List<SingleShift> GroupOfShifts { get; set; }

        public ShiftGroup (int ShiftNumber, List<SingleShift> GroupOfShifts)
        {
            this.ShiftNumber = ShiftNumber;
            this.GroupOfShifts = GroupOfShifts;
        }
    }
}
