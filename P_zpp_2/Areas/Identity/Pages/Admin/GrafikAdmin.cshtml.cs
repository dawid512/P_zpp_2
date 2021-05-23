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


namespace P_zpp_2.Areas.Identity.Pages.Admin
{
    public class GrafikAdminModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;
        

        public GrafikAdminModel(P_zpp_2DbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public List<EventModel> _ScheduleDaysList { get; set; }
        public string _callMeJson { get; set; }

        public void OnGet()
        {
            GenerateSchedule();

            //var tmp = DisplayShiftOF(User.Identity.GetUserId(), string ScheduleName);

            //_ScheduleDaysList = 


            _callMeJson = JsonSerializer.Serialize(_ScheduleDaysList);


        }
        public List<EventModel> converter(List<SimpleDisplayShifs> toConvertList)
        {
            return null;
        }
        public void GenerateSchedule()
        {
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
        }
    }
}
