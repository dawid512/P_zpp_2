using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class AlgoritmSettings
    {
        public int CashRegistersToMan { get; set; }
        public int OpeningShift { get; set; }
        public int MorningShift { get; set; }
        public int RushHourShift { get; set; }
        public int EveningShift { get; set; }
        public int ClosingShift { get; set; }
        public int RushShiftRotation { get; set; }

        public AlgoritmSettings(int cashRegistersToMan)
        {
            var workersRequired = cashRegistersToMan * 2 - 1;
            if (workersRequired == 1) workersRequired++;
            CashRegistersToMan = cashRegistersToMan;


            if (workersRequired == 2)
            {
                OpeningShift = 1;
                MorningShift = 0;
                RushHourShift = 0;
                EveningShift = 0;
                ClosingShift = 1;
            }
            else if (workersRequired == 3)
            {
                OpeningShift = 1;
                MorningShift = 0;
                RushHourShift = 1;
                EveningShift = 0;
                ClosingShift = 1;
            }
            else
            {
                var rush = workersRequired % 4;
                var quarter = workersRequired - rush / 4;
                OpeningShift = quarter;
                MorningShift = quarter;
                RushHourShift = rush;
                EveningShift = quarter;
                ClosingShift = quarter;
            }
        }
        public void PrintAlgoritmSettings()
        {
            Console.WriteLine("CashRegistersToMan: " + CashRegistersToMan);
            Console.WriteLine("OpeningShift:       " + OpeningShift);
            Console.WriteLine("MorningShift:       " + MorningShift);
            Console.WriteLine("RushHourShift:      " + RushHourShift);
            Console.WriteLine("EveningShift:       " + EveningShift);
            Console.WriteLine("ClosingShift:       " + ClosingShift);
        }
    }
}
