using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class LastShiftInfo
    {
        public int ShiftNumber { get; set; }
        public int DaysToGenerate { get; set; }
        public int TeamNumber { get; set; }
        public int ShiftType { get; set; }
        public LastShiftInfo(int ShiftNumber, int DaysToGenerate, int ShiftType)
        {
            this.ShiftNumber = ShiftNumber;
            this.DaysToGenerate = DaysToGenerate;
            this.ShiftType = ShiftType;
        }
        public LastShiftInfo(int ShiftNumber, int DaysToGenerate, int TeamNumber, int ShiftType)
        {
            this.ShiftNumber = ShiftNumber;
            this.DaysToGenerate = DaysToGenerate;
            this.TeamNumber = TeamNumber;
            this.ShiftType = ShiftType;
        }
    }
}
