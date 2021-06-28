using System;
using System.Collections.Generic;
using System.Text;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    /// <summary>
    /// Contains info of workday date, and personel at each shift
    /// </summary>
    public class Workday
    {
        public DateTime Date { get; set; }

        public List<Nurse> CrewDay { get; set; }
        public List<Nurse> CrewNight { get; set; }
        public List<Nurse> CrewOffStation { get; set; }

        public Workday(DateTime date, List<Nurse> crewDay, List<Nurse> crewNight, List<Nurse> offSiteCrew)
        {
            Date = date;
            CrewDay = crewDay;
            CrewNight = crewNight;
            CrewOffStation = offSiteCrew;
        }

        /*
        public void PrintAll()
        {
            Console.WriteLine("\n---------------------------------\n-----------" + this.Date.Date + "----------\n---------------------------------");
            PrintDayshift();
            PrintNightshift();
            PrintOffStation();
        }
        public void PrintDayshift()
        {
            Console.WriteLine($"\n-----------Dayshift-----------({this.CrewDay.Count})");
            foreach (var item in this.CrewDay)
                item.Print();
        }
        public void PrintNightshift()
        {
            Console.WriteLine($"\n-----------Nighshift-----------({this.CrewNight.Count})");
            foreach (var item in this.CrewNight)
                item.Print();
        }
        public void PrintOffStation()
        {
            Console.WriteLine($"\n-----------CrewOffStation-----------({ this.CrewOffStation.Count})");
            foreach (var item in this.CrewOffStation)
                item.Print();
        }*/
    }
}
