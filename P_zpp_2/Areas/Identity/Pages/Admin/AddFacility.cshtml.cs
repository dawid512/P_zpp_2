using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ScheduleAlgoritms.NursesAlgoritm.Items;
using static Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.ExternalLoginModel;

namespace P_zpp_2.Areas.Identity.Pages.Admin.Pracownicy
{
    public class AddFacilityModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;
        [BindProperty]
        public InputModel Input { get; set; }


        public AddFacilityModel(P_zpp_2DbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        public IEnumerable<Company> _myCompany { get; set; }
        public IEnumerable<Departures> _myDeparture{ get; set; }
        public void OnGet()
        {
            _myCompany = _context.company.ToList();
            _myDeparture = _context.departures.ToList();
        }
    }
}
