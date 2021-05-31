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
using P_zpp_2.Areas.Identity.Pages.Account.Display_Interfaces;

namespace P_zpp_2.Areas.Identity.Pages.Pracownik
{
    public class GrafikPracownikModel : PageModel
    {
        private readonly AplicationUser _context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<ApplicationUser> _UserManager;

        public GrafikPracownikModel(AplicationUser context, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Configuration = configuration;
            _UserManager = userManager;
        }

        public List<SimpleDisplayShifs> _ScheduleDaysListSDS { get; set; }
        public ApplicationUser pracownik { get; set; }
        public int licznik { get; set; }
        public IWorkerScheduleDisplay Schedule_type { get; set; }
        public void OnGet()
        {           
            licznik = 0;
            var uid = _UserManager.GetUserId(User); 
            var ScheduleName = _context.Users.Find(uid);
            
            GetScheduletype(_context.Users.Find(uid).Rola);
            
            // _ScheduleDaysListSDS = Schedule_type.GetSchedule(_context, uid, ScheduleName.Schedule);
              
            DemoScheduleToDispaly();
        }

        public void GetScheduletype(string search_schedule)
        {
            switch (search_schedule)
            {
                case "Nurse":
                    {
                        Schedule_type = new NurseWorkerDispaly();
                        break;
                    };
            }
        }

        /*public List<EventModel> Converter(List<SimpleDisplayShifs> toConvertList)
        {
            List<EventModel> tmp = new List<EventModel>();
            int iterator = 1;
            foreach (var item in toConvertList)
            {
                tmp.Add(new EventModel(iterator, item.Title, item.StartDate.ToString("yyyy-MM-dd")));
            }

            return tmp;
        }*/
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
