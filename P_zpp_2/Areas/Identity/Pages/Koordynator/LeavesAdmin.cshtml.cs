﻿using System;
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
using P_zpp_2.Models.MyCustomLittleDatabase;

namespace P_zpp_2.Areas.Identity.Pages.Koordynator
{
    [Authorize(Roles = "Administrator , Koordynator")]
    public class LeavesAdminModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly IConfiguration Configuration;

        public LeavesAdminModel(P_zpp_2DbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }


        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Leaves> Leaves { get; set; }

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

            IQueryable<Leaves> pracownikIQ = from s in _context.leaves
                                                      select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pracownikIQ = pracownikIQ.Where(s => s.Idusera.LastName.Contains(searchString)
                                       || s.Idusera.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    pracownikIQ = pracownikIQ.OrderByDescending(s => s.Idusera.LastName);
                    break;
                case "Imie":
                    pracownikIQ = pracownikIQ.OrderBy(s => s.Idusera.FirstName);
                    break;
                case "username":
                    pracownikIQ = pracownikIQ.OrderByDescending(s => s.Idusera.UserName);
                    break;
                default:
                    pracownikIQ = pracownikIQ.OrderBy(s => s.Idusera.LastName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Leaves = await PaginatedList<Leaves>.CreateAsync(
                pracownikIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        [HttpPost]
        public ActionResult ConfirmAppointment(int appointmentID)
        {
            var appt = _context.leaves.Find(appointmentID);
            appt.Status_zaakceptopwane = true;
            _context.SaveChanges();
            return RedirectToAction(CurrentSort);
        }
    }
}
