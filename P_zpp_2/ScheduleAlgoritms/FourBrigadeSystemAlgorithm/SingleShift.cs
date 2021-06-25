using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class SingleShift
    {
        public DateTime ShiftBegin { get; set; }
        public DateTime ShiftEnd { get; set; }

        public SingleShift(DateTime ShiftBegin, DateTime ShiftEnd)
        {
            this.ShiftBegin = ShiftBegin;
            this.ShiftEnd = ShiftEnd;
        }
    }
}
