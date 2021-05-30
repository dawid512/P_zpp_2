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

namespace P_zpp_2.Areas.Identity.Pages.Pracownik
{
    public class GrafikPracownikModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _UserManager;

        public GrafikPracownikModel(P_zpp_2DbContext context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _UserManager = userManager;
        }

        public List<EventModel> _ScheduleDaysList { get; set; }
        public List<SimpleDisplayShifs> _ScheduleDaysListSDS { get; set; }
        public string _callMeJson { get; set; }
        public ApplicationUser pracownik { get; set; }
        public int licznik { get; set; }
        public void OnGet()
        {
            DemoScheduleToDispaly();
            licznik = 0;



            var uid = _UserManager.GetUserId(User);
            var tmp = new NursesMain();
            var ScheduleName = _context.Users.Find(uid);
            _callMeJson = JsonSerializer.Serialize(Converter(tmp.DisplayShiftOF(_context, uid, ScheduleName.Schedule)));

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
        public void DemoScheduleToDispaly()
        {
             _ScheduleDaysListSDS = new List<SimpleDisplayShifs> {
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
        }
    }
}
