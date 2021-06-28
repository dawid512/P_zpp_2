using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm;
using P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;
using P_zpp_2.ScheduleAlgoritms.RetailAlgoritm.Structures;
using P_zpp_2.ScheduleAlgoritms.RetailAlgoritm;

namespace P_zpp_2.Areas.Identity.Pages.Admin
{
    public class GrafikAdminModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _userManager;


        public GrafikAdminModel(P_zpp_2DbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }

        public List<EventModel> _ScheduleDaysList { get; set; }
        public string _callMeJson { get; set; }

        

        public async void OnGetAsync(ApplicationUser user, int ScheduleId)
        {
            GenerateSchedule();

           // var deps = _context.company.Select(x => x);
            var actualCoordinatorID = await _userManager.GetUserIdAsync(user);
            
            //scheduleInstructions = _context.ScheduleInstructions/*.Where(x => x.CoordinatorId == actualCoordinatorID)*/.ToList();
            // scheduleInstructions = new SelectList(coordinators, "UserId", "LastName");


            demo("4554f19d-99db-46b8-b9a8-33b76c64b160",20, DateTime.Now, "retail");
            var schedule = _context.schedules.Where(x => x.CoordinatorId== actualCoordinatorID && x.ScheduleName== "retail").First();
            //var schedule = _context.schedules.Where(x => x.Id == 4).First();

            switch (schedule.ScheduleName)
            {
                case "fbs":
                    DeserializeFourBrigadeSystemScheduleToEventModelList(schedule.ScheduleInJSON);
                    break;
                case "retail":
                    _callMeJson = RetailConverter(schedule.Id);
                    break;
                case "nurse": 
                    break;
            }


            _callMeJson = JsonConvert.SerializeObject(_ScheduleDaysList);


        }
        public void demo(string coordinatorId, int numberOfays, DateTime generateFromDate, string schedulename)
        {

            var AD = new AlgoritmData(
                new AlgoritmSettings(4),
                new List<RetailWorker>(),
                new ShiftInfo());
            RetailAlgoritm.CreateNewSettingsAndSaveToDatabase(AD, schedulename, coordinatorId, _context);

            var CEO = _context.Users.Find(coordinatorId);
            List<RetailWorkday> currentSchedule = RetailAlgoritm.GetCurrentScheduleFromDatabaseInJson(schedulename, coordinatorId,_context);
            var updatedPersonel = RetailAlgoritm.UpdateNewPersonel(
                                        RetailAlgoritm.UpdateLegacyPersonel_RemoveFired_AddNew(RetailAlgoritm.GetCurrentPersonel((int)CEO.DeptId), AD.retailWorkerList, AD),
                                        AD.algoritmSettings
                                    );
            List<RetailWorkday> newSchedule = RetailAlgoritm.FillEmptyScheduleWithWorkers(
                                                    RetailAlgoritm.GenerateEmptySchedule(numberOfays, generateFromDate),
                                                    AD.retailWorkerList,
                                                    AD.algoritmSettings
                                                    );
            currentSchedule.AddRange(newSchedule);
            RetailAlgoritm.Save_Complete_Schedule_To_Database(currentSchedule, coordinatorId, schedulename,AD);
            RetailAlgoritm.Save_New_AlgoritmData_To_Database(AD, schedulename, coordinatorId);
        }
        public string RetailConverter(int scheduleId)
        {
            

            var schedule = _context.schedules.Where(x => x.Id == scheduleId).First();
            var workdaylist = JsonConvert.DeserializeObject<List<RetailWorkday>>(schedule.ScheduleInJSON);
            List<EventModel> toJson = new();

            foreach (var workday in workdaylist)
            {
                foreach (var item in workday.MorningShift)
                {
                    toJson.Add(new EventModel(1, 1 + " " + item.WorkerName, workday.DayDate.ToString("yyyy-MM-dd")));
                }
                foreach (var item in workday.OpeningShift)
                {
                    toJson.Add(new EventModel(1, 2 + " " + item.WorkerName, workday.DayDate.ToString("yyyy-MM-dd")));
                }
                foreach (var item in workday.RushHourShift)
                {
                    toJson.Add(new EventModel(1, 3 + " " + item.WorkerName, workday.DayDate.ToString("yyyy-MM-dd")));
                }
                foreach (var item in workday.EveningShift)
                {
                    toJson.Add(new EventModel(1, 4 + " " + item.WorkerName, workday.DayDate.ToString("yyyy-MM-dd")));
                }
                foreach (var item in workday.ClosingShift)
                {
                    toJson.Add(new EventModel(1, 5 + " " + item.WorkerName, workday.DayDate.ToString("yyyy-MM-dd")));
                }
            }

            return JsonConvert.SerializeObject(toJson);
        }
        private List<EventModel> DeserializeFourBrigadeSystemScheduleToEventModelList(string scheduleInJSON)
        {
            List<EventModel> EventModelList = new();
            
            var deserializedSchedule = JsonConvert.DeserializeObject<Dictionary<ApplicationUser, List<SingleShift>>>(scheduleInJSON);
            var listOfKeys = deserializedSchedule.Keys.ToList();
            foreach (var item in deserializedSchedule)
            {
                foreach (var item2 in item.Value)
                {
                    EventModelList.Add(new EventModel(listOfKeys.IndexOf(item.Key), item.Key.FirstName + " " + item.Key.LastName, item2.ShiftBegin.ToString()));
                }
            }
            return EventModelList;
        }

        
        public void GenerateSchedule()
        {
            _ScheduleDaysList = new List<EventModel>{
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(1, "2 zmiana", "2021-05-10"),
                 new EventModel(2, "1 zmiane", "2021-05-12"),
                 new EventModel(1, "2 zmiana", "2021-05-13"),
                 new EventModel(2, "1 zmiane", "2021-05-15"),
                 new EventModel(1, "2 zmiana", "2021-05-16"),
                 new EventModel(2, "1 zmiane", "2021-05-18"),
                 new EventModel(2, "2 zmiane", "2021-05-19"),
                 new EventModel(2, "1 zmiane", "2021-05-21"),
                 new EventModel(2, "2 zmiane", "2021-05-22"),
                 new EventModel(2, "1 zmiane", "2021-05-24"),
                 new EventModel(2, "2 zmiane", "2021-05-25"),
                 new EventModel(2, "1 zmiane", "2021-05-27"),
                 new EventModel(2, "2 zmiane", "2021-05-28")
            };
        }
    }
}
