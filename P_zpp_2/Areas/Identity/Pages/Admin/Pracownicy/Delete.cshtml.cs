using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;

namespace P_zpp_2.Areas.Identity.Pages.Admin.Pracownicy
{
    public class DeleteModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly ILogger<DetailsModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(P_zpp_2DbContext context,
                           ILogger<DetailsModel> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser Pracownik { get; set; }
        public Departures Dzialy { get; set; }

        public async Task<IActionResult> OnGetAsync(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Pracownik = await _context.Users.FindAsync(Id);
            Dzialy = _context.departures.Where(x => x.DeprtureId == Pracownik.DeptId).FirstOrDefault();

            if (Pracownik == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string? Id)
        {
            var pracownikToDelete = await _context.Users.FindAsync(Id);



            if (pracownikToDelete == null)
            {
                return NotFound();
            }
            try
            {
                _context.Users.Remove(pracownikToDelete);
                await _context.SaveChangesAsync();
                return RedirectToPage("./PracownicyAdmin");
            }
            catch (DbUpdateException ex)
            {

               

                return RedirectToAction("./Delete",
                                     new { Id, saveChangesError = true });
            }


            await _context.SaveChangesAsync();
            return RedirectToPage("./PracownicyAdmin");
        }


    }
}

