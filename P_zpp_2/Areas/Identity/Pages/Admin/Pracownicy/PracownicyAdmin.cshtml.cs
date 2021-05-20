using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P_zpp_2.Controllers;
using P_zpp_2.Data;
using P_zpp_2.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace P_zpp_2.Areas.Identity.Pages.Admin
{

    [Authorize(Roles = "Administrator , Koordynator")]

    public class PracownicyAdminModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;

        public PracownicyAdminModel(P_zpp_2DbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<ApplicationUser> Users { get; set; }

        //public IList<ApplicationUser> Users { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Imie" ? "username" : "Imie";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            

            CurrentFilter = searchString;

            IQueryable<ApplicationUser> pracownikIQ = from s in _context.Users
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pracownikIQ = pracownikIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pracownikIQ = pracownikIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Imie":
                    pracownikIQ = pracownikIQ.OrderBy(s => s.FirstName);
                    break;
                case "username":
                    pracownikIQ = pracownikIQ.OrderByDescending(s => s.UserName);
                    break;
                default:
                    pracownikIQ = pracownikIQ.OrderBy(s => s.LastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Users = await PaginatedList<ApplicationUser>.CreateAsync(
                pracownikIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
