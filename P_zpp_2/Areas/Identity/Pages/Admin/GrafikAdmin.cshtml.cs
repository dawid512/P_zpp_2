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
           

           // var deps = _context.company.Select(x => x);
            var actualCoordinatorID = await _userManager.GetUserIdAsync(user);
            var schedule = _context.schedules.Where(x => x.Id == 4).First();
            //scheduleInstructions = _context.ScheduleInstructions/*.Where(x => x.CoordinatorId == actualCoordinatorID)*/.ToList();
            // scheduleInstructions = new SelectList(coordinators, "UserId", "LastName");
            GenerateSchedule(schedule);
            switch (schedule.ScheduleName)
            {
                case "fbs":
                    DeserializeFourBrigadeSystemScheduleToEventModelList(schedule.ScheduleInJSON);
                    break;
            }


            _callMeJson = JsonConvert.SerializeObject(_ScheduleDaysList);


        }

        private List<EventModel> DeserializeFourBrigadeSystemScheduleToEventModelList(string scheduleInJSON)
        {
            List<EventModel> EventModelList = new();
            
            var deserializedSchedule1 = JsonConvert.DeserializeObject<Dictionary<string, List<SingleShift>>>(scheduleInJSON);
            var deserializedSchedule = new Dictionary<ApplicationUser, List<SingleShift>>();

            foreach (var item in deserializedSchedule1)
            {
                var user = _context.Users.Where(x => x.UserName == item.Key).First();
                deserializedSchedule.Add(user, item.Value);
            }

            var listOfKeys = deserializedSchedule.Keys.ToList();
            foreach (var item in deserializedSchedule)
            {
                foreach (var item2 in item.Value)
                {
                    EventModelList.Add(new EventModel(listOfKeys.IndexOf(item.Key), item2.ShiftBegin.Hour+":"+item2.ShiftBegin.Minute+"0 -"+item2.ShiftEnd.Hour + ":" + item2.ShiftEnd.Minute+"0, " + item.Key.FirstName + " " + item.Key.LastName, item2.ShiftBegin.Date.ToString("yyyy-MM-dd")));
                }
            }
            return EventModelList;
        }

        public List<EventModel> converter(List<SimpleDisplayShifs> toConvertList)
        {
            
            
            return null;
        }
        public void GenerateSchedule(Schedule schedule)
        {
            _ScheduleDaysList = DeserializeFourBrigadeSystemScheduleToEventModelList(schedule.ScheduleInJSON);
        }
    }
}
