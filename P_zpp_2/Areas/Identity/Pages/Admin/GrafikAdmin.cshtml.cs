using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using P_zpp_2.Data;
using P_zpp_2.Models.MyCustomLittleDatabase;

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
            _ScheduleDaysList =new List<EventModel>{
                new EventModel(1, "nornica", "2021-05-16"),
                 new EventModel(2, "lwiczka", "2021-05-18")
            };
            var tmp = _ScheduleDaysList;
            _callMeJson = JsonSerializer.Serialize(tmp);


        }
    }
}
