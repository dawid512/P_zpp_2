using P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using P_zpp_2.Data;
namespace P_zpp_2.ScheduleAlgoritms.RetailAlgoritm
{
    public class RetailAlgoritm
    {
    /// <summary>
    /// Method meant to show how to implememnt algoritm in mvc-controller/razorPage
    /// </summary>
    /// <param name="numberOfays">number of day to generate shedule for</param>
    /// <param name="generateFromDate">first day from which schedule will be generated from</param>
    /// <param name="scheduleName"></param>
       private static void RetailMain(int numberOfays, DateTime generateFromDate, string scheduleName)//wszystko z tej funkcji leci do widoku
        {
            /*
            int tmpVariable_MustBeDeletedBeforeImplementationsOfFInalAlgoritm = 4; //to będzie pobierane z View do tworzenia
            string algoritmConfigJson = Get_Config_From_Database(scheduleName);

            AlgoritmData config;
            if (algoritmConfigJson == null || algoritmConfigJson == string.Empty) config = CreateNewSettingsAndSaveToDatabase(tmpVariable_MustBeDeletedBeforeImplementationsOfFInalAlgoritm, scheduleName);
            else config = JsonConvert.DeserializeObject<AlgoritmData>(algoritmConfigJson);

            var updatedPersonel = UpdateNewPersonel(
                                        UpdateLegacyPersonel_RemoveFired_AddNew(GetCurrentPersonel(), config.retailWorkerList),
                                        config.algoritmSettings
                                    );
            if (updatedPersonel.Any()) config.retailWorkerList = updatedPersonel; //complete list of current employees that will be fed into algoritm

            List<RetailWorkday> currentSchedule = GetCurrentScheduleFromDatabase();

            List<RetailWorkday> newSchedule = FillEmptyScheduleWithWorkers(
                                                    GenerateEmptySchedule(numberOfays, generateFromDate),
                                                    config.retailWorkerList,
                                                    config.algoritmSettings
                                                    );
            currentSchedule.AddRange(newSchedule);
            Save_Complete_Schedule_To_Database(currentSchedule);
            Save_New_AlgoritmData_To_Database(config);*/
        }
        //----------------rquires database---------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates list of currently employed eployees in database, and assigns algoritm controll parameters
        /// </summary>
        /// <param name="retailWorkers">current workes from applicationUser</param>
        /// <param name="legacyWorkers">eployees working in previous days</param>
        /// <param name="AD"></param>
        /// <returns></returns>
        private static List<RetailWorker> UpdateLegacyPersonel_RemoveFired_AddNew(List<RetailWorker> retailWorkers, List<RetailWorker> legacyWorkers, AlgoritmData AD)
        {
            var oldToBeRemovedFromLegacy = new List<RetailWorker>();
            
            //znajduje zwolnionych
            bool currentlyEmployed = false;
            foreach (var lw in legacyWorkers)
            {
                currentlyEmployed = false;
                foreach (var cw in retailWorkers)
                {
                    if (lw.WorkerId == cw.WorkerId) 
                    {
                        currentlyEmployed = true; 
                        break;
                    }
                }
                if (!currentlyEmployed)
                    oldToBeRemovedFromLegacy.Add(lw);
            }
            //usuwa zwolnionych
            foreach (var item in oldToBeRemovedFromLegacy)
            {
                legacyWorkers.Remove(item);
            }
            //szuka nowych
            var newWorker = new List<RetailWorker>();
            foreach (var lw in legacyWorkers)
            {
                if (retailWorkers.Contains(lw)) continue;
                else newWorker.Add(lw);
            }
            foreach (var item in newWorker)
            {
                legacyWorkers.Add(item);
            }

            AD.retailWorkerList = UpdateNewPersonel(legacyWorkers, AD.algoritmSettings);
            return AD.retailWorkerList;

        }
        /// <summary>
        /// Retrieves chosen schedule from database and deserializes it from json
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <param name="coordinatorID"></param>
        /// <returns></returns>
        private static List<RetailWorkday> GetCurrentScheduleFromDatabaseInJson(string scheduleName, string coordinatorID)
        {
            using (var db = new P_zpp_2DbContext())
            {
                var tmp = db.schedules.Where(x => x.CoordinatorId == coordinatorID && scheduleName == x.ScheduleName).First();
                return JsonConvert.DeserializeObject<List<RetailWorkday>>(tmp.ScheduleInJSON);
            }
        }
        /// <summary>
        /// saves newly created algoritmdata parameter class to dabase as string
        /// </summary>
        /// <param name="newAD"></param>
        /// <param name="scheduleName"></param>
        /// <param name="coordinatorID"></param>
        private static void Save_New_AlgoritmData_To_Database(AlgoritmData newAD, string scheduleName, string coordinatorID)
        {
            using (var db = new P_zpp_2DbContext())
            {
                var toEdit = db.schedules.Where(x => x.CoordinatorId == coordinatorID && x.ScheduleName == scheduleName).First();
                toEdit.ScheduleInstructions = JsonConvert.SerializeObject(newAD);
                db.schedules.Update(toEdit);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// creates new entity in schedules table and saves algoritmData to it
        /// </summary>
        /// <param name="newAD"></param>
        /// <param name="scheduleName"></param>
        /// <param name="coordinatorID"></param>
        private static void CreateNewSettingsAndSaveToDatabase(AlgoritmData newAD, string scheduleName, string coordinatorID) //skonsultować szymona lub szymona
        {
            using (var db = new P_zpp_2DbContext())
            {
                db.schedules.Add(new Schedule(scheduleName, string.Empty, JsonConvert.SerializeObject(newAD), coordinatorID));
                db.SaveChanges();
            }
        }
        /// <summary>
        /// Saves schedule to database ans string
        /// </summary>
        /// <param name="completeSchedule"></param>
        /// <param name="coordinatorId"></param>
        /// <param name="scheduleName"></param>
        /// <param name="config"></param>
        private static void Save_Complete_Schedule_To_Database(List<RetailWorkday> completeSchedule, string coordinatorId, string scheduleName, AlgoritmData config)
        {
            using (var db = new P_zpp_2DbContext())
            {
                // check for existibg schedule, and if not...
                if (db.schedules.Where(x => x.CoordinatorId == coordinatorId && x.ScheduleName == scheduleName).Any())
                {
                    var toEdit = db.schedules.Where(x => x.CoordinatorId == coordinatorId && x.ScheduleName == scheduleName).First();
                    toEdit.ScheduleInJSON = JsonConvert.SerializeObject(completeSchedule);
                    db.schedules.Update(toEdit);
                    db.SaveChanges();
                }
                else //...just make fucking new one
                {
                    db.schedules.Add(
                        new Schedule(scheduleName, JsonConvert.SerializeObject(completeSchedule), JsonConvert.SerializeObject(config), coordinatorId)
                        );
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// Retrieves active personel from applicationUser from database
        /// </summary>
        /// <param name="CoordinatorDeptID"></param>
        /// <returns></returns>
        private static List<RetailWorker> GetCurrentPersonel(int CoordinatorDeptID)
        {
            List<RetailWorker> Slaves = new();
            using (var db = new P_zpp_2DbContext())
            {
                var personelApplicationUser = db.Users.Where(x => x.DeptId == CoordinatorDeptID).ToList();
                foreach (var slave in personelApplicationUser)
                    Slaves.Add(Convert_ApplicationUser_To_RetailWorker(slave));
            }
            return Slaves;
        }
        /// <summary>
        /// Retrieves AlgoritmData from database if exists
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <param name="coordinatorId"></param>
        /// <returns></returns>
        private static string Get_Config_From_Database(String scheduleName, string coordinatorId)
        {
            try
            {
                string result = string.Empty;
                using (var db = new P_zpp_2DbContext())
                {
                    result = db.schedules.Where(sm => sm.ScheduleName == scheduleName && sm.CoordinatorId == coordinatorId).Select(x => x.ScheduleInstructions).First(); //tu wstawić poprawne wyszukiwanie grafikow
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// converts applicationUsers entities from database into RetailWorker class
        /// </summary>
        /// <param name="slave"></param>
        /// <returns></returns>
        private static RetailWorker Convert_ApplicationUser_To_RetailWorker(Models.ApplicationUser slave)
        {
            using (var db = new P_zpp_2DbContext())
            {
                List<DateTime> workDaysOff = new();
                var freeDays = db.leaves.Where(x => x.Idusera.Id == slave.Id && x.Status_zaakceptopwane == true).ToList();
                foreach (var item in freeDays)
                {
                    var ammountOfDays = item.CheckOut - item.CheckIn;
                    for (int i = 0; i < ammountOfDays.Days; i++)
                        workDaysOff.Add(item.CheckIn.AddDays(i));
                }

                return new RetailWorker
                {
                    WorkerId = slave.Id,
                    WorkerLastShift = 0,
                    WorkerName = slave.FirstName + " " + slave.LastName,
                    OffWorkDays = workDaysOff,
                };
            }
        }
        /// <summary>
        /// Updates newly employeed personel with algoritm controll parameters 
        /// </summary>
        /// <param name="allPersonel"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static List<RetailWorker> UpdateNewPersonel(List<RetailWorker> allPersonel, AlgoritmSettings config)
        {
            if (allPersonel.Where(person => person.WorkerLastShift < 6 && person.WorkerLastShift > 0).Any())
            {
                List<RetailWorker> newWorkerList = allPersonel.Where(person => person.WorkerLastShift < 6 && person.WorkerLastShift > 0).ToList();
                foreach (var person in newWorkerList)
                {
                    if (allPersonel.Where(person => person.WorkerLastShift == 1).Count() != config.ClosingShift)
                    {
                        person.WorkerLastShift = 1;
                        continue;
                    }
                    if (allPersonel.Where(person => person.WorkerLastShift == 2).Count() != config.MorningShift)
                    {
                        person.WorkerLastShift = 2;
                        continue;
                    }
                    if (allPersonel.Where(person => person.WorkerLastShift == 3).Count() != config.EveningShift)
                    {
                        person.WorkerLastShift = 3;
                        continue;
                    }
                    if (allPersonel.Where(person => person.WorkerLastShift == 4).Count() != config.RushHourShift)
                    {
                        person.WorkerLastShift = 4;
                        continue;
                    }
                    if (allPersonel.Where(person => person.WorkerLastShift == 5).Count() != config.OpeningShift)
                    {
                        person.WorkerLastShift = 5;
                        continue;
                    }
                }
                return newWorkerList;
            }
            return new List<RetailWorker>();
        }
        /// <summary>
        /// Fills empty schedule workdays with personel 
        /// </summary>
        /// <param name="shedule"></param>
        /// <param name="allPersonel"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private static List<RetailWorkday> FillEmptyScheduleWithWorkers(List<RetailWorkday> shedule, List<RetailWorker> allPersonel, AlgoritmSettings config)
        {

            foreach (var day in shedule)
            {
                if (day.DayDate.DayOfWeek.ToString() == "Monday")
                    foreach (var person in allPersonel)
                    {
                        person.WorkerLastShift++;
                        if (person.WorkerLastShift == 6) person.WorkerLastShift = 1;
                    }

                List<RetailWorker> AvailablePersonel = new();
                foreach (var person in allPersonel)
                    if (person.OffWorkDays.Contains(day.DayDate))
                        AvailablePersonel.Add(person);

                foreach (var person in AvailablePersonel)
                    switch (person.WorkerLastShift)
                    {
                        case 1:
                            if (day.ClosingShift.Count <= config.ClosingShift) day.ClosingShift.Add(person);
                            continue;
                        case 2:
                            if (day.MorningShift.Count <= config.MorningShift) day.MorningShift.Add(person);
                            continue;
                        case 3:
                            if (day.EveningShift.Count <= config.EveningShift) day.EveningShift.Add(person);
                            continue;
                        case 4:
                            if (day.RushHourShift.Count <= config.RushHourShift) day.RushHourShift.Add(person);
                            continue;
                        case 5:
                            if (day.OpeningShift.Count <= config.OpeningShift) day.OpeningShift.Add(person);
                            continue;
                    }
            }
            return shedule;
        }
        /// <summary>
        /// Creates empty schedule to be filled with personel
        /// </summary>
        /// <param name="numberOfDays"></param>
        /// <param name="generateFromDate"></param>
        /// <returns></returns>
        private static List<RetailWorkday> GenerateEmptySchedule(int numberOfDays, DateTime generateFromDate)
        {
            for (int i = 0; i < numberOfDays; i++)
            {
                if (generateFromDate.AddDays(i).DayOfWeek.ToString() == "Sunday") continue;
                Console.WriteLine(generateFromDate.AddDays(i).DayOfWeek); //prints in console, for testing only
            }
            return new List<RetailWorkday>();
        }
    }
}
