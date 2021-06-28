using CashierAlgorithm.Database;
using Newtonsoft.Json;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm
{/// <summary>
/// Class containing all the methods necessary to generate the schedule.
/// </summary>
/// <remarks>
/// Class main method is <see cref="Generate(string, ScheduleInstructions, int, P_zpp_2DbContext, DateTime, DateTime)"/>. It sets in motion the whole process of generating the schedule.
/// </remarks>
    public class FourBrigadeSystem
    {
        private Dictionary<ApplicationUser, List<SingleShift>> _Schedule;
        public List<KeyValuePair<ApplicationUser, List<SingleShift>>> SerializedSchedule
        {
            get { return _Schedule.ToList(); }
            set { _Schedule = value.ToDictionary(x => x.Key, x => x.Value); }
        }
        /// <summary>
        /// Main method that start all the method necessary to generate the schedule in four brigade work system.
        /// </summary>
        /// <param name="coordinatorId">Coordinator ID.</param>
        /// <param name="si">Schedule instructions created and chosen by coordinator.</param>
        /// <param name="departmentId">Work department ID</param>
        /// <param name="db">Database context.</param>
        /// <param name="d1">First day of schedule to generate.</param>
        /// <param name="d2">Last day of schedule to generate.</param>
        /// <returns>Schedule of type <see cref="Dictionary{ApplicationUser, List{SingleShift}}"/> that is <see href="https://www.newtonsoft.com/json/help/html/SerializingJSON.htm">serialized in JSON.</see></returns>
        public string Generate(string coordinatorId, ScheduleInstructions si, int departmentId, P_zpp_2DbContext db, DateTime d1, DateTime d2)
        {
            //List<ShiftInfoForScheduleGenerating> sifsg = new();
            //sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 6, 0, 0), new DateTime(2020, 5, 31, 14, 0, 0),  4,  false)); // 6-14
            //sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 14, 0, 0), new DateTime(2020, 5, 31, 22, 0, 0),  4,  false)); //14-22
            //sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 22, 0, 0), new DateTime(2020, 5, 31, 6, 0, 0),  4,  true));//22-6

            d1 = new DateTime(2021, 6, 1);
            d2 = new DateTime(2021, 6, 30);

            var teams = GetTeamsToAlgorithm(departmentId, db);
            var shiftInfoFromUser = JsonConvert.DeserializeObject<List<ShiftInfoForScheduleGenerating>>(si.ListOfShistsInJSON);
            var lastSchedule = GetLastSchedule(shiftInfoFromUser.First().ShiftSetBeginTime.Date, db);
            var shifts = CreateShifts(shiftInfoFromUser, lastSchedule, d1, d2);
            var ScheduleInDictionary = PutTeamsIntoShifts(shifts, teams, lastSchedule, shiftInfoFromUser);

            var ScheduleWithoutLeaves = GenerateSchedule(ScheduleInDictionary, teams);
            _Schedule = AdjustScheduleForLeaves(ScheduleWithoutLeaves, si.CoordinatorId, db);
            string json = JsonConvert.SerializeObject(SerializedSchedule, Formatting.Indented);
            return json;
        }
        /// <summary>
        /// Gets the last schedule that has ending date right before starting day of a schedule that is about to be generated.
        /// </summary>
        /// <param name="StartingDay">First day of a schedule to generate</param>
        /// <param name="db">An instance of DbContext</param>
        /// <returns>Returns a <see cref="CustomScheduleClass"/> object, that contains unserialized Schedule and HangingDays property.</returns>
        private CustomScheduleClass GetLastSchedule(DateTime StartingDay, P_zpp_2DbContext db)
        {

            var DayBeforeStartingDay = StartingDay.AddDays(-1);
            var lastschedule = db.schedules
                .Where(x => x.LastScheduleDay == DayBeforeStartingDay.Date)
                .FirstOrDefault();

            if (lastschedule != null)
            {
                var ScheduleInDictionary = JsonConvert.DeserializeObject<Dictionary<ApplicationUser, List<SingleShift>>>(lastschedule.ScheduleInJSON);
                var HangingDays = JsonConvert.DeserializeObject<List<LastShiftInfo>>(lastschedule.HangingDaysInJSON);
                CustomScheduleClass scheduleToReturn = new CustomScheduleClass(ScheduleInDictionary, HangingDays);
                return scheduleToReturn;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// Takes the already created <see cref="ShiftGroup"/>s and puts <see cref="Team"/>s teams in it.
        /// </summary>
        /// <param name="shifts">Tuple containing two items: <see cref="List{}"/> of <see cref="ShiftGroup"/> items, and <see cref="List{T}"/> of <see cref="LastShiftInfo"/>.</param>
        /// <param name="teams"><see cref="List{T}"/> of <see cref="Team"/>s that contains <see cref="ApplicationUser"/>s.</param>
        /// <param name="lastSchedule"><see cref="CustomScheduleClass"/> object that contains information about "hanging days"</param>
        /// <param name="shiftInfo"><see cref="List{T}"/> of <see cref="ShiftInfoForScheduleGenerating"/> which contains instructions about creating the shifts.</param>
        /// <returns><see cref="Tuple{T1, T2}"/> build with item1 as <see cref="Dictionary{ShiftGroup, TValue}"/> of key <see cref="ShiftGroup"/> and value of <see cref="Team"/>, and item2 as <see cref="List{T}"/> of <see cref="LastShiftInfo"/></returns>
        private Tuple<Dictionary<ShiftGroup, Team>, List<LastShiftInfo>> PutTeamsIntoShifts(Tuple<List<ShiftGroup>, List<LastShiftInfo>> shifts, List<Team> teams, CustomScheduleClass lastSchedule, List<ShiftInfoForScheduleGenerating> shiftInfo)
        {
            int x = 0;
            var ScheduleInDictionary = new Dictionary<ShiftGroup, Team>();
            List<LastShiftInfo> lastShiftInfos = new();
            int y;
            if (lastSchedule == null)
            {
                y = 0;
            }
            else
            {
                foreach (var item in lastSchedule.HangingDays)
                {
                    ScheduleInDictionary.Add(shifts.Item1[lastSchedule.HangingDays.IndexOf(item)], teams[item.TeamNumber]);
                }
                y = lastSchedule.HangingDays.Count;
                x = lastSchedule.HangingDays.Last().TeamNumber;
                if (x == 3)
                {
                    x = 0;
                }
            }
            for (int i = y; i < shifts.Item1.Count; i++)
            {
                ScheduleInDictionary.Add(shifts.Item1[i], teams[x]);
                if (x == 3)
                {
                    x = 0;
                }
                else
                {
                    x++;
                }
            }
            var last3 = ScheduleInDictionary.Skip(ScheduleInDictionary.Count - 5).ToList();
            foreach (var item in last3)
            {
                lastShiftInfos.Add(new LastShiftInfo(item.Key.ShiftNumber, shiftInfo.First().ShiftLengthInDays, item.Value.TeamNumber, last3.IndexOf(item)));
            }
            return Tuple.Create(ScheduleInDictionary, lastShiftInfos);
        }
        /// <summary>
        /// Takes the generated schedule and removes the dates from <see cref="Dictionary{TKey, TValue}"/> if they are present in <see cref="Leaves"/> table, and then saves it to database.
        /// </summary>
        /// <param name="schedule">Tuple containing Schedule in dictionary and list of LastShiftInfo.</param>
        /// <param name="coordinatorId">Id of coordinator that generates the schedule.</param>
        /// <param name="db">Instance of database context.</param>
        /// <returns>A schedule ready to be serialized.</returns>
        private Dictionary<ApplicationUser, List<SingleShift>> AdjustScheduleForLeaves(Tuple<Dictionary<ApplicationUser, List<SingleShift>>, List<LastShiftInfo>> schedule, string coordinatorId, P_zpp_2DbContext db)
        {
            List<Leaves> leaves = new();
            var firstDay = schedule.Item1.First().Value.First().ShiftBegin;

            foreach (var item in schedule.Item1)
            {
                leaves = db.leaves.Where(x => x.Idusera.Id == item.Key.Id && x.Status_zaakceptopwane == true && x.CheckOut > firstDay).ToList();

                foreach (var leave in leaves)
                {
                    foreach (var item2 in item.Value)
                    {
                        if ((item2.ShiftBegin >= leave.CheckIn) && (item2.ShiftEnd <= leave.CheckOut))
                        {
                            item.Value.Remove(item2);
                        }
                    }
                }
            }
            var x = "";
            var eee = schedule.Item1.Last().Value.Last().ShiftBegin.Date;
            var yyy = JsonConvert.SerializeObject(schedule.Item1);
            var uuu = coordinatorId;
            Schedule sch = new(coordinatorId, "tes2t", schedule.Item1.Last().Value.Last().ShiftBegin.Date, JsonConvert.SerializeObject(schedule.Item1), null, "fbs");
            

            db.schedules.Add(sch);
            db.SaveChanges();
            return schedule.Item1;

        }
        /// <summary>
        /// Takes the grouped shifts and teams which are in them and creates a schedule in type of <see cref="Dictionary{TKey, TValue}"/> with key as <see cref="ApplicationUser"/> and value as <see cref="List{T}"/> of <see cref="SingleShift"/>
        /// </summary>
        /// <param name="ScheduleInDictionary">Schedule in dictionary</param>
        /// <param name="teams">Teams of workers</param>
        /// <returns>Tuple with schedule in dictionary as item1, and List{T} of LastShiftInfo containing info about last shifts.</returns>
        private Tuple<Dictionary<ApplicationUser, List<SingleShift>>, List<LastShiftInfo>> GenerateSchedule(Tuple<Dictionary<ShiftGroup, Team>, List<LastShiftInfo>> ScheduleInDictionary, List<Team> teams)
        {
            var schedule = new Dictionary<ApplicationUser, List<SingleShift>>();
            var workerlist = new List<ApplicationUser>();
            workerlist = teams.SelectMany(x => x.TeamMembers).ToList();


            List<SingleShift> temp = new();
            foreach (var item in workerlist)
            {
                foreach (var item2 in ScheduleInDictionary.Item1)
                {
                    if (item2.Value.TeamMembers.Contains(item))
                    {

                        var xyz = item2.Key.GroupOfShifts;
                        temp.AddRange(xyz);
                    }
                }
                var tempAdd = temp.ConvertAll(x => (SingleShift)x);
                schedule.Add(item, tempAdd);
                temp.Clear();
            }

            return Tuple.Create(schedule, ScheduleInDictionary.Item2);
        }
        
        /// <summary>
        /// Takes all the users assigned to department and checks if they are assigned to teams, and runs other methods that assign all the unassigned workers.
        /// </summary>
        /// <param name="departmentId">Id of coordinator's department.</param>
        /// <param name="db">Instance of database context.</param>
        /// <returns><see cref="List{T}"/> of <see cref="Team"/>s.</returns>
        private List<Team> GetTeamsToAlgorithm(int departmentId, P_zpp_2DbContext db)
        {

            var workerlist = new List<ApplicationUser>();


            workerlist = db.Users
                .Where(x => x.DeptId == departmentId)
                .ToList();

            List<ApplicationUser>? AssignedWorkers = workerlist
                .Where(x => x.SpecialInfo != null)
                .ToList();

            List<ApplicationUser>? AssignedWorkers2 = AssignedWorkers
                .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber != null)
                .ToList();


            if (!AssignedWorkers2.Any() || AssignedWorkers2 == null) //workers have never been assigned
            {
                return AssignAllWorkersToTeams(workerlist, db);
            }
            else if (AssignedWorkers2.Count != workerlist.Count) //new workers were added to database
            {
                return AssignNewWorkersToAlreadyExistingTeams(workerlist, db);
            }
            else
            {
                return GetTeams(workerlist);
            }


        }
        /// <summary>
        /// Method started by <see cref="GetTeamsToAlgorithm(int, P_zpp_2DbContext)"/> method, when all the workers for which the schedule will be generated are assigned to the team.
        /// </summary>
        /// <param name="workerlist"><see cref="List{T}"/> of <see cref="ApplicationUser"/>s</param>
        /// <returns><see cref="List{T}"/> of <see cref="Team"/>s</returns>
        private List<Team> GetTeams(List<ApplicationUser> workerlist)
        {
            List<Team> Teams = new();
            for (int i = 0; i < 4; i++)
            {
                var team = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == i + 1)
                    .ToList();
                Teams.Add(new Team(i + 1, team));
            }
            return Teams;
        }
        /// <summary>
        /// Assignes all the workers which weren't assigned to the team to the team with the least amount of workers.
        /// </summary>
        /// <param name="workerlist"><see cref="List{T}"/> of <see cref="ApplicationUser"/>s</param>
        /// <param name="db">An instance of database context.</param>
        /// <returns><see cref="List{T}"/> of <see cref="Team"/>s</returns>
        private List<Team> AssignNewWorkersToAlreadyExistingTeams(List<ApplicationUser> workerlist, P_zpp_2DbContext db)
        {
            List<Team> Teams = new();
            for (int i = 0; i < 4; i++)
            {
                var team = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == i + 1)
                    .ToList();
                Teams.Add(new Team(i + 1, team));
            }
            var unassignedworkers = workerlist.Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber == null);

            foreach (var item in unassignedworkers)
            {
                var TeamToAdd = Teams.OrderBy(x => x.TeamMembers.Count).First();
                item.SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo { TeamNumber = TeamToAdd.TeamNumber });
                db.Update(item);
                Teams[Teams.IndexOf(TeamToAdd)].TeamMembers.Add(item);
            }
            db.SaveChanges();


            return Teams;
        }
        /// <summary>
        /// Method that creates <see cref="List{T}"/> of <see cref="Team"/>s with assigned workers to teams.
        /// </summary>
        /// <param name="workerlist"><see cref="List{T}"/> of <see cref="ApplicationUser"/>s</param>
        /// <param name="db">An instance of database context.</param>
        /// <returns></returns>
        private List<Team> AssignAllWorkersToTeams(List<ApplicationUser> workerlist, P_zpp_2DbContext db)
        {
            List<Team> Teams = new();
            var WorkerAmount = workerlist.Count / 4;
           
                for (int i = 0; i < 4; i++)
                {
                    var team = workerlist.Skip(i * WorkerAmount).Take(WorkerAmount).ToList();
                    foreach (var item in team)
                    {
                        item.SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo { TeamNumber = i + 1 });
                        db.Update(item);
                    }
                    Teams.Add(new Team(i + 1, team));
                }
                db.SaveChanges();
            
            return Teams;
        }
        /// <summary>
        /// Method that runs either <see cref="CreateShiftsWithHangingDays(List{ShiftInfoForScheduleGenerating}, CustomScheduleClass, List{LastShiftInfo}, List{ShiftGroup}, DateTime, DateTime)">CreateShiftWithHangingDays</see> method or <see cref="CreateShiftsWithoutHangingDays(List{ShiftInfoForScheduleGenerating}, List{LastShiftInfo}, List{ShiftGroup}, DateTime, DateTime)">CreateShiftsWithoutHaningDays</see> method, regarding if "lastschedule" parameter is null
        /// </summary>
        /// <param name="shiftInfoForScheduleGenerating">List of instructions for generating shifts.</param>
        /// <param name="lastSchedule">Last schedule from the database</param>
        /// <param name="d1">Date of beggining of schedule.</param>
        /// <param name="d2">Date of ending of schedule.</param>
        /// <returns>Tuple with grouped list and list of LastShiftInfo.</returns>
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShifts(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, CustomScheduleClass lastSchedule, DateTime d1, DateTime d2)
        {
            List<LastShiftInfo> ListOfLastShiftInfos = new();
            List<ShiftGroup> GroupedShifts = new();
            if (lastSchedule == null)
            {
                var tuple = CreateShiftsWithoutHangingDays(shiftInfoForScheduleGenerating, ListOfLastShiftInfos, GroupedShifts, d1, d2);
                return tuple;
            }
            else
            {
                var tuple = CreateShiftsWithHangingDays(shiftInfoForScheduleGenerating, lastSchedule, ListOfLastShiftInfos, GroupedShifts, d1, d2);
                return tuple;
            }
        }
        /// <summary>
        /// Creates shifts when there are no hanging days.
        /// </summary>
        /// <param name="shiftInfoForScheduleGenerating"></param>
        /// <param name="ListOfLastShiftInfos"></param>
        /// <param name="GroupedShifts"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>Tuple with grouped list and list of LastShiftInfo.</returns>
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShiftsWithoutHangingDays(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, List<LastShiftInfo> ListOfLastShiftInfos, List<ShiftGroup> GroupedShifts, DateTime d1, DateTime d2)
        {
            var dateTimeList = new List<SingleShift>();
            var dateTimeListToAdd = new List<SingleShift>();
            bool isFirstShiftOfItsType = false;
            int ShiftNumber = 0;
            var time = (d2 - d1).Days;
            foreach (var item in shiftInfoForScheduleGenerating)
            {
                for (int i = d1.Date.Day; i <= time; i++)
                {
                    var begin = new DateTime(DateTime.Now.Year, 5, i, item.ShiftSetBeginTime.Hour, item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (item.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, 5, i, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == d2.Day)
                        {
                            end = new DateTime(DateTime.Now.Year, 5 + 1, 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, 5, i + 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                    }

                    dateTimeList.Add(new SingleShift(begin, end));
                    if ((dateTimeList.Count == item.ShiftLengthInDays) || i == time)
                    {
                        dateTimeListToAdd = dateTimeList.ConvertAll(x => (SingleShift)x);
                        if (isFirstShiftOfItsType == false)
                        {
                            ShiftNumber = shiftInfoForScheduleGenerating.IndexOf(item) + 1;
                            GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));
                            isFirstShiftOfItsType = true;
                        }
                        else
                        {
                            ShiftNumber += 3;
                            GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));

                            if (i == time)
                            {
                                ListOfLastShiftInfos.Add(new LastShiftInfo(ShiftNumber, item.ShiftLengthInDays - dateTimeListToAdd.Count, shiftInfoForScheduleGenerating.IndexOf(item)));
                            }

                        }
                        dateTimeList.Clear();
                    }
                }
                isFirstShiftOfItsType = false;
                ShiftNumber = 0;
            }
            GroupedShifts = (List<ShiftGroup>)GroupedShifts.OrderBy(x => x.ShiftNumber).ToList();
            return Tuple.Create(GroupedShifts, ListOfLastShiftInfos);

        }
        /// <summary>
        /// Creates shifts when there are hanging days in last schedule.
        /// </summary>
        /// <param name="shiftInfoForScheduleGenerating"></param>
        /// <param name="lastSchedule"></param>
        /// <param name="ListOfLastShiftInfos"></param>
        /// <param name="GroupedShifts"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns>Tuple with grouped list and list of LastShiftInfo.</returns>
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShiftsWithHangingDays(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, CustomScheduleClass lastSchedule, List<LastShiftInfo> ListOfLastShiftInfos, List<ShiftGroup> GroupedShifts, DateTime d1, DateTime d2)
        {
            var dateTimeList = new List<SingleShift>();
            var dateTimeListToAdd = new List<SingleShift>();
            bool isFirstShiftOfItsType = false;
            int ShiftNumber = 0;
            var time = (d2 - d1).Days;
            foreach (var item in lastSchedule.HangingDays)
            {
                var shiftInfo = shiftInfoForScheduleGenerating[item.ShiftType];
                for (int i = 1; i <= item.DaysToGenerate; i++)
                {
                    var begin = new DateTime(DateTime.Now.Year, 5, i, shiftInfo.ShiftSetBeginTime.Hour, shiftInfo.ShiftSetBeginTime.Minute, shiftInfo.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (shiftInfo.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, 5, i, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == 31)
                        {
                            end = new DateTime(DateTime.Now.Year, 6, 1, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, 5, i + 1, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
                        }
                    }
                    dateTimeList.Add(new SingleShift(begin, end));
                    dateTimeListToAdd = dateTimeList.ConvertAll(x => (SingleShift)x);
                    if (isFirstShiftOfItsType == false)
                    {
                        ShiftNumber = shiftInfoForScheduleGenerating.IndexOf(shiftInfo) + 1;
                        GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));
                        isFirstShiftOfItsType = true;
                    }
                    else
                    {
                        ShiftNumber += 3;
                        GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));
                    }
                    dateTimeList.Clear();
                }
            }
            foreach (var item in shiftInfoForScheduleGenerating)
            {
                for (int i = lastSchedule.HangingDays.First().DaysToGenerate + 1; i <= time; i++)
                {
                    var begin = new DateTime(DateTime.Now.Year, 5, i, item.ShiftSetBeginTime.Hour, item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (item.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, 5, i, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == 31)
                        {
                            end = new DateTime(DateTime.Now.Year, 6, 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, 5, i + 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                    }

                    dateTimeList.Add(new SingleShift(begin, end));
                    if ((dateTimeList.Count == item.ShiftLengthInDays) || i == time)
                    {
                        dateTimeListToAdd = dateTimeList.ConvertAll(x => (SingleShift)x);
                        if (isFirstShiftOfItsType == false)
                        {
                            ShiftNumber = shiftInfoForScheduleGenerating.IndexOf(item) + 1;
                            GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));
                            isFirstShiftOfItsType = true;
                        }
                        else
                        {
                            ShiftNumber += 3;
                            GroupedShifts.Add(new ShiftGroup(ShiftNumber, dateTimeListToAdd));

                            if (i == time)
                            {
                                ListOfLastShiftInfos.Add(new LastShiftInfo(ShiftNumber, item.ShiftLengthInDays - dateTimeListToAdd.Count, shiftInfoForScheduleGenerating.IndexOf(item)));
                            }

                        }
                        dateTimeList.Clear();
                    }
                }
                isFirstShiftOfItsType = false;
                ShiftNumber = 0;
            }
            GroupedShifts = (List<ShiftGroup>)GroupedShifts.OrderBy(x => x.ShiftNumber).ToList();

            return Tuple.Create(GroupedShifts, ListOfLastShiftInfos);
        }
        private List<ShiftInfoForScheduleGenerating> GetShiftInfoFromUser()
        {

            List<ShiftInfoForScheduleGenerating> sifsg = new();
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 6, 0, 0), new DateTime(2020, 5, 31, 14, 0, 0), 4, false)); // 6-14
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 14, 0, 0), new DateTime(2020, 5, 31, 22, 0, 0),  4,  false)); //14-22
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 22, 0, 0), new DateTime(2020, 5, 31, 6, 0, 0),  4,  true));//22-6

            return sifsg;
        }
    }
    /// <summary>
    /// Internal class for creating Leaves objects.
    /// </summary>
    internal class LeavesForProcessing
    {
        public List<DateTime> LeaveDayRange { get; set; }
        public string WorkerId { get; set; }

        internal LeavesForProcessing(Leaves leave)
        {
            LeaveDayRange = JsonConvert.DeserializeObject<List<DateTime>>(leave.LeaveDayRange);
            WorkerId = leave.Idusera.Id;
        }
    }
}

