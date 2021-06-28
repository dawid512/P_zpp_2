using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures
{
    public class RetailWorkday
    {
        public DateTime DayDate { get; set; }
        public List<RetailWorker> OpeningShift { get; set; } //1
        public List<RetailWorker> MorningShift { get; set; } //2
        public List<RetailWorker> RushHourShift { get; set; } //3
        public List<RetailWorker> EveningShift { get; set; } //4
        public List<RetailWorker> ClosingShift { get; set; } //5

        public void PrintRetailWorkday()
        {
            Console.WriteLine(DayDate + " " + DayDate.DayOfWeek);

            Print(OpeningShift, "OpeningShift");
            Print(MorningShift, "MorningShift");
            Print(RushHourShift, "RushHourShift");
            Print(EveningShift, "EveningShift");
            Print(ClosingShift, "ClosingShift");

        }
        public void Print(List<RetailWorker> itemsToPrint, String shiftName)
        {
            Console.WriteLine(shiftName);
            foreach (var person in itemsToPrint)
                Console.WriteLine("   " + person);
        }
    }
}
