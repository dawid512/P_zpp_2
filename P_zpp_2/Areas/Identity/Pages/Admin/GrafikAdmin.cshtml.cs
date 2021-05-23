using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;
using P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items;

namespace P_zpp_2.Areas.Identity.Pages.Admin
{
    public class GrafikAdminModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _UserManager;

        public GrafikAdminModel(P_zpp_2DbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _UserManager = userManager;
        }

        public List<EventModel> _ScheduleDaysList { get; set; }
        public string _callMeJson { get; set; }
        public ApplicationUser pracownik { get; set; }

        public void OnGet()
        {

            var uid = _UserManager.GetUserId(User);
            var tmp = new NursesMain();
            var ScheduleName = _context.Users.Find(uid);
            
            //_callMeJson = JsonSerializer.Serialize(Converter(tmp.DisplayShiftOF(_context ,uid, ScheduleName.Schedule)));
            _callMeJson = JsonSerializer.Serialize(Converter(GenerateSampleSchedule()));

            //GenerateSampleSchedule();
            //tmp.DisplayShiftOF(UserManager.GetUserId(User),)
            //_callMeJson = JsonSerializer.Serialize(_ScheduleDaysList);
        }

        public List<EventModel> Converter(List<SimpleDisplayShifs> toConvertList)
        {
            List<EventModel> tmp = new List<EventModel>();
            int iterator = 1;
            foreach (var item in toConvertList)
            {
                tmp.Add(new EventModel(iterator, item.Title, item.StartDate.ToString("yyyy-MM-dd")));
            }

            return tmp;
        }
        public List<SimpleDisplayShifs> GenerateSampleSchedule()
        {
            var _user = _context.Users.Find(_UserManager.GetUserId(User));
            
            _ScheduleDaysList = new List<EventModel>{
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

            var shifts = new List<SimpleDisplayShifs> {
                new SimpleDisplayShifs(DateTime.Now.Date, "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(1), "2 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(3), "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(4), "2 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(6), "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(7), "2 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(9), "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(10), "2 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(12), "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(13), "2 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(15), "1 zmiana"),
                new SimpleDisplayShifs(DateTime.Now.Date.AddDays(16), "2 zmiana"),
                };
            return shifts;
        }
    }
}
