using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    public class NursesMain
    {
        /*public Task RunNUrsesScheduler(int NumberOfDaysToSchedule)
        {
            var PersonelList = new List<Nurse>();
            try
            {
                using (var db = new *//*databasename*//*)
                {
                    var personelFromFatabase = db.Roles.Where(x => x.Name == "Nurse").ToList();
                    foreach (var item in personelFromFatabase)
                        PersonelList.Add(ToNurseConverter(item));
                }
            }
            catch { }
            var Schedule = new List<Workday>();
            for (int i = 0; i < WardWorkInfo.WorkCycleLenght; i++)
                SelectStaff(Schedule, PersonelList, i);
        }*/

        /*private Nurse ToNurseConverter(object item)
        {
            return new Nurse(
                item.id,
                item.name,
                item.overflowdayshifts,
                item.overflownightshifts,
                item.overflowoffdays,
                CalculateLeave(item)
                );
        }*/

        /*private List<DateTime> CalculateLeave(object item)
        {

            return new List<DateTime> Spisdniwolnych;
        }*/

        public static void SelectStaff(List<Workday> workdaysList, List<Nurse> Personel, int Nofdays)
        {
            var today = DateTime.Now.Date.AddDays(Nofdays);
            var PeopleWhoHaveLeaveToday = Personel.Where(n => n.AprovedLeave.Contains(today)).ToList();
            var AvailableStaff = Personel.Except(PeopleWhoHaveLeaveToday).ToList(); // xD
            var day = SelectForDayShift(AvailableStaff);
            var night = SelectForNightShift(AvailableStaff, day);
            var off = SetDayOff(AvailableStaff, day, night);

            workdaysList.Add(new Workday(today, day, night, off));
            var wd = new Workday(DateTime.Now.Date.AddDays(Nofdays), day, night, off);

        }
        public static List<Nurse> SelectForDayShift(List<Nurse> Personel)
        {
            var tmp = Personel.Where(n => n.DayshiftsCompleted == Personel.Min(m => m.DayshiftsCompleted) && n.LastShift_night != true).Take(WardWorkInfo.Workers_RequiredforDayshift).ToList();

            while (tmp.Count < (WardWorkInfo.Workers_RequiredforDayshift - 1))
            {
                tmp.Add(Personel.Except(tmp).Where(n => n.DaysOff == Personel.Except(tmp).Max(m => m.DaysOff) && n.LastShift_night != true).FirstOrDefault());
            }

            foreach (var item in tmp)
                item.DayshiftsCompleted++;

            foreach (var item in Personel)
                item.LastShift_night = false;

            return tmp;
        }
        public static List<Nurse> SelectForNightShift(List<Nurse> Personel, List<Nurse> CurrentlyOccupied)
        {
            var nonOcupied = Personel.Except(CurrentlyOccupied).ToList();
            var tmp = nonOcupied.Where(n => n.NightshitCompleted == Personel.Min(m => m.NightshitCompleted)).Take(WardWorkInfo.Workers_RequiredforNightshift).ToList();
            foreach (var item in tmp)
            {
                item.LastShift_night = true;
                item.NightshitCompleted++;
            }
            while (tmp.Count < 2)
            {
                tmp.Add(nonOcupied.Except(tmp).Where(n => n.DaysOff == nonOcupied.Except(tmp).Max(m => m.DaysOff)).FirstOrDefault());
            }
            return tmp;
        }
        public static List<Nurse> SetDayOff(List<Nurse> Personel, List<Nurse> CurrentlyOccupied_Day, List<Nurse> CurrentlyOccupied_night)
        {
            var tmp = Personel.Except(CurrentlyOccupied_Day).ToList();
            var tmp2 = tmp.Except(CurrentlyOccupied_night).ToList();

            foreach (var item in tmp2)
                item.DaysOff++;

            return tmp2;
        }
    }
}
