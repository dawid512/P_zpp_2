using P_zpp_2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P_zpp_2.Areas.Identity.Data;
using System.IO;
using Newtonsoft.Json;

namespace P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items
{
    public class NursesMain
    {        
        public List<SimpleDisplayShifs> DisplayShiftOF (string User_id, string ScheduleName)
        {
            using(var db = new P_zpp_2DbContext())
            {
                List<SimpleDisplayShifs> tmp = new List<SimpleDisplayShifs>();
                var masterSchedule = db.schedules.Where(x => x.scheduleName == ScheduleName).FirstOrDefault();

                var masterScheduleDeserialized = JsonConvert.DeserializeObject<List<Workday>>(File.ReadAllText(masterSchedule.jsonfilewithschedule_locaton));

                foreach (var item in masterScheduleDeserialized)
                {
                    if(item.CrewDay.Where(x=>x.Id == User_id).Any())
                    {
                        tmp.Add(new SimpleDisplayShifs(item.Date, "Day"));
                    }else if (item.CrewNight.Where(x => x.Id == User_id).Any())
                    {
                        tmp.Add(new SimpleDisplayShifs(item.Date, "Night"));
                    }
                    else if (item.CrewOffStation.Where(x => x.Id == User_id).Any())
                    {
                        tmp.Add(new SimpleDisplayShifs(item.Date, "Off"));
                    }
                    else
                    {
                        tmp.Add(new SimpleDisplayShifs(item.Date, "Blad grafiku, prosze o kontakt z koordynatorem."));
                    }
                }
                return tmp;
            }
        }
        public void RunNUrsesScheduler(int NumberOfDaysToSchedule, string NameOfSchedule)
        {
            using (var db = new P_zpp_2DbContext())
            {
                var PersonelList = new List<Nurse>();

                if (!db.schedules.Where(schedule => schedule.scheduleName == "Nurse").Any())
                {
                    PersonelList = PrepareListOfNursesOutOfUsersFromDatabase(db.Users.Where(x => x.Rola == "Nurse").ToList());
                }
                else
                {
                    var StringToDeserialize = File.ReadAllText(
                        db.schedules
                            .Where(schedules => schedules.scheduleName == NameOfSchedule)
                            .Select(x => x.jsonfilewithschedule_locaton)
                            .ToString()
                        );

                    PersonelList = JsonConvert.DeserializeObject<List<Nurse>>(StringToDeserialize);
                }

                var Schedule = new List<Workday>();
                for (int i = 0; i < WardWorkInfo.WorkCycleLenght; i++)
                    SelectStaff(Schedule, PersonelList, i);

                PrepareDataForStorage(Schedule, PersonelList, NameOfSchedule);


            }
        }
        private void PrepareDataForStorage(List<Workday> wd, List<Nurse> pl, string NameOfSchedule)
        {
            var serialized_workdays = JsonConvert.SerializeObject(wd);
            var serialized_personel_algoritm_info = JsonConvert.SerializeObject(pl);
            var Json_file_location_schedule = "Environment.GetFolderPath(Environment.SpecialFolder.Desktop)" + @"\NurseSchedules\" + NameOfSchedule + ".txt";
            var Json_file_location_staff = "Environment.GetFolderPath(Environment.SpecialFolder.Desktop)" + @"\NurseSchedules\" + NameOfSchedule + "_staff" + ".txt";

            File.WriteAllText(Json_file_location_schedule, serialized_workdays);
            File.WriteAllText(Json_file_location_staff, serialized_personel_algoritm_info);
            SaveScheduleLocationToDatabase(NameOfSchedule, Json_file_location_schedule, Json_file_location_staff);
        }
        private void SaveScheduleLocationToDatabase( string Schedule_name, string schedule_location, string staff_data_location )
        {
            using (var db = new P_zpp_2DbContext())
            {
                if(db.schedules.Where(x=>x.scheduleName == Schedule_name).Any())
                {
                    db.schedules.Remove(db.schedules.Where(x => x.scheduleName == Schedule_name).FirstOrDefault());
                }
                else
                {
                    db.schedules.Add(new Schedule(Schedule_name, staff_data_location, schedule_location));
                }
                db.SaveChanges();
            }
        }

        public List<Nurse> PrepareListOfNursesOutOfUsersFromDatabase(List<ApplicationUser> list)//run only once
        {
            List<Nurse> tmp = new List<Nurse>();
            foreach (var item in list)
                tmp.Add(new Nurse(item.Id, item.LastName + " " + item.FirstName, 0, 0, 0, null));
            return tmp;
        }

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
