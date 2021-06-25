using CashierAlgorithm.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashierAlgorithm.Algorithms.FourBrigadeSystem
{
    public class FourBrigadeSystem : ISchedule
    {
        private Dictionary<Workers, List<SingleShift>> _Schedule;
        public List<KeyValuePair<Workers, List<SingleShift>>> SerializedSchedule
        {
            get { return _Schedule.ToList(); }
            set { _Schedule = value.ToDictionary(x => x.Key, x => x.Value); }
        }

        public string Generate()
        {
            var teams = GetTeamsToAlgorithm();
            var shiftInfoFromUser = GetShiftInfoFromUser();
            var lastSchedule = GetLastSchedule(shiftInfoFromUser.First().ShiftSetBeginTime.Date);
            var shifts = CreateShifts(shiftInfoFromUser, lastSchedule);
            var ScheduleInDictionary = PutTeamsIntoShifts(shifts, teams, lastSchedule, shiftInfoFromUser);

            var ScheduleWithoutLeaves = GenerateSchedule(ScheduleInDictionary, teams);
            _Schedule = AdjustScheduleForLeaves(ScheduleWithoutLeaves, shiftInfoFromUser.First().coordinatorId);
            string json = JsonConvert.SerializeObject(SerializedSchedule, Formatting.Indented);
            return json;
        }
        private Schedule GetLastSchedule(DateTime StartingDay)
        {
            using (var db = new TmpContext())
            {
                var DayBeforeStartingDay = StartingDay.AddDays(-1);
                Schedules? lastschedule = db.Schedules
                    .Where(x => x.LastScheduleDay.Date == DayBeforeStartingDay.Date)
                    .FirstOrDefault();

                if (lastschedule != null)
                {
                    var ScheduleInDictionary = JsonConvert.DeserializeObject<Dictionary<Workers, List<SingleShift>>>(lastschedule.ScheduleInJSON);
                    var HangingDays = JsonConvert.DeserializeObject<List<LastShiftInfo>>(lastschedule.HangingDaysInJSON);
                    Schedule scheduleToReturn = new Schedule(ScheduleInDictionary, HangingDays);
                    return scheduleToReturn;
                }
                else
                {
                    return null;
                }
            }
        }
        private Tuple<Dictionary<ShiftGroup, Team>, List<LastShiftInfo>> PutTeamsIntoShifts(Tuple<List<ShiftGroup>, List<LastShiftInfo>> shifts, List<Team> teams, Schedule lastSchedule, List<ShiftInfoForScheduleGenerating> shiftInfo)
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
        private Dictionary<Workers, List<SingleShift>> AdjustScheduleForLeaves(Tuple<Dictionary<Workers, List<SingleShift>>, List<LastShiftInfo>> schedule, int coordinatorId)
        {
            List<Leaves> leaves = new();
            var firstDay = schedule.Item1.First().Value.First().ShiftBegin;
            using (var db = new TmpContext())
            {
                foreach (var item in schedule.Item1)
                {
                    leaves = db.Leaves.Where(x => x.WorkerId == item.Key.Id && x.Status == true && x.LeaveEnd > firstDay).ToList();

                    foreach (var leave in leaves)
                    {
                        foreach (var item2 in item.Value)
                        {
                            if ((item2.ShiftBegin >= leave.LeaveStart) && (item2.ShiftEnd <= leave.LeaveEnd))
                            {
                                item.Value.Remove(item2);
                            }
                        }
                    }
                }
                Schedules sch = new()
                {
                    CoordinatorId = coordinatorId,
                    LastScheduleDay = schedule.Item1.Last().Value.Last().ShiftBegin.Date,
                    Name = "test",
                    ScheduleInJSON = JsonConvert.SerializeObject(schedule.Item1),
                    HangingDaysInJSON = JsonConvert.SerializeObject(schedule.Item2)
                };

                db.Schedules.Add(sch);
                //db.SaveChanges();
                return schedule.Item1;
            }
        }
        private Tuple<Dictionary<Workers, List<SingleShift>>, List<LastShiftInfo>> GenerateSchedule(Tuple<Dictionary<ShiftGroup, Team>, List<LastShiftInfo>> ScheduleInDictionary, List<Team> teams)
        {
            var schedule = new Dictionary<Workers, List<SingleShift>>();
            var workerlist = new List<Workers>();
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
        public List<DateTime> GetWorkingDays()
        {
            throw new NotImplementedException();
        }
        private List<Team> GetTeamsToAlgorithm()
        {

            var workerlist = new List<Workers>();

            using (var db = new TmpContext())
            {
                workerlist = db.Workers
                    .Where(x => x.DepartmentId == 49)
                    .ToList();

                var AssignedWorkers = workerlist
                    .Where(x => JsonConvert.DeserializeObject<SpecialInfo>(x.SpecialInfo).TeamNumber != null)
                    .ToList();

                if (!AssignedWorkers.Any()) //workers have never been assigned
                {
                    return AssignAllWorkersToTeams(workerlist);
                }
                else if (AssignedWorkers.Count != workerlist.Count) //new workers were added to database
                {
                    ;
                    return AssignNewWorkersToAlreadyExistingTeams(workerlist);
                }
                else
                {
                    return GetTeams(workerlist);
                }
            }


        }
        private List<Team> GetTeams(List<Workers> workerlist)
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
        private List<Team> AssignNewWorkersToAlreadyExistingTeams(List<Workers> workerlist)
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
            using (var db = new TmpContext())
            {
                foreach (var item in unassignedworkers)
                {
                    var TeamToAdd = Teams.OrderBy(x => x.TeamMembers.Count).First();
                    item.SpecialInfo = JsonConvert.SerializeObject(new SpecialInfo { TeamNumber = TeamToAdd.TeamNumber });
                    db.Update(item);
                    Teams[Teams.IndexOf(TeamToAdd)].TeamMembers.Add(item);
                }
                db.SaveChanges();
            }

            return Teams;
        }
        private List<Team> AssignAllWorkersToTeams(List<Workers> workerlist)
        {
            List<Team> Teams = new();
            var WorkerAmount = workerlist.Count / 4;
            using (var db = new TmpContext())
            {
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
            }
            return Teams;
        }
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShifts(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, Schedule lastSchedule)
        {
            List<LastShiftInfo> ListOfLastShiftInfos = new();
            List<ShiftGroup> GroupedShifts = new();
            if (lastSchedule == null)
            {
                var tuple = CreateShiftsWithoutHangingDays(shiftInfoForScheduleGenerating, ListOfLastShiftInfos, GroupedShifts);
                return tuple;
            }
            else
            {
                var tuple = CreateShiftsWithHangingDays(shiftInfoForScheduleGenerating, lastSchedule, ListOfLastShiftInfos, GroupedShifts);
                return tuple;
            }
        }
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShiftsWithoutHangingDays(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, List<LastShiftInfo> ListOfLastShiftInfos, List<ShiftGroup> GroupedShifts)
        {
            var dateTimeList = new List<SingleShift>();
            var dateTimeListToAdd = new List<SingleShift>();
            bool isFirstShiftOfItsType = false;
            int ShiftNumber = 0;
            var time = DateTime.DaysInMonth(2020, 8);
            foreach (var item in shiftInfoForScheduleGenerating)
            {
                for (int i = 1; i <= time; i++)
                {
                    var begin = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetBeginTime.Hour, item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (item.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == 31)
                        {
                            end = new DateTime(DateTime.Now.Year, item.ScheduleMonth + 1, 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i + 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
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
        private Tuple<List<ShiftGroup>, List<LastShiftInfo>> CreateShiftsWithHangingDays(List<ShiftInfoForScheduleGenerating> shiftInfoForScheduleGenerating, Schedule lastSchedule, List<LastShiftInfo> ListOfLastShiftInfos, List<ShiftGroup> GroupedShifts)
        {
            var dateTimeList = new List<SingleShift>();
            var dateTimeListToAdd = new List<SingleShift>();
            bool isFirstShiftOfItsType = false;
            int ShiftNumber = 0;
            var time = DateTime.DaysInMonth(2020, 8);
            foreach (var item in lastSchedule.HangingDays)
            {
                var shiftInfo = shiftInfoForScheduleGenerating[item.ShiftType];
                for (int i = 1; i <= item.DaysToGenerate; i++)
                {
                    var begin = new DateTime(DateTime.Now.Year, shiftInfo.ScheduleMonth, i, shiftInfo.ShiftSetBeginTime.Hour, shiftInfo.ShiftSetBeginTime.Minute, shiftInfo.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (shiftInfo.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, shiftInfo.ScheduleMonth, i, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == 31)
                        {
                            end = new DateTime(DateTime.Now.Year, shiftInfo.ScheduleMonth + 1, 1, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, shiftInfo.ScheduleMonth, i + 1, shiftInfo.ShiftSetEndTime.Hour, shiftInfo.ShiftSetEndTime.Minute, shiftInfo.ShiftSetEndTime.Second);
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
                    var begin = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetBeginTime.Hour, item.ShiftSetBeginTime.Minute, item.ShiftSetBeginTime.Second);
                    DateTime end;
                    if (item.IsOvernight == false)
                    {
                        end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                    }
                    else
                    {
                        if (i == 31)
                        {
                            end = new DateTime(DateTime.Now.Year, item.ScheduleMonth + 1, 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
                        }
                        else
                        {
                            end = new DateTime(DateTime.Now.Year, item.ScheduleMonth, i + 1, item.ShiftSetEndTime.Hour, item.ShiftSetEndTime.Minute, item.ShiftSetEndTime.Second);
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
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 6, 0, 0), new DateTime(2020, 5, 31, 14, 0, 0), 8, 4, 1, false)); // 6-14
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 14, 0, 0), new DateTime(2020, 5, 31, 22, 0, 0), 8, 4, 1, false)); //14-22
            sifsg.Add(new ShiftInfoForScheduleGenerating(new DateTime(2020, 5, 1, 22, 0, 0), new DateTime(2020, 5, 31, 6, 0, 0), 8, 4, 1, true));//22-6

            return sifsg;
        }
    }
    internal class LeavesForProcessing
    {
        public List<DateTime> LeaveDayRange { get; set; }
        public int WorkerId { get; set; }

        internal LeavesForProcessing(Leaves leave)
        {
            LeaveDayRange = JsonConvert.DeserializeObject<List<DateTime>>(leave.LeaveDayRange);
            WorkerId = leave.WorkerId;
        }
    }
}

