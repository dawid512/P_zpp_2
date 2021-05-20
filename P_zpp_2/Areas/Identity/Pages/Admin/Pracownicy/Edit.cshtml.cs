using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using P_zpp_2.Data;
using P_zpp_2.Models;

namespace P_zpp_2.Areas.Identity.Pages.Admin.Pracownicy
{
    public class EditModel : PageModel
    {
        private readonly P_zpp_2DbContext _context;
        private readonly ILogger<EditModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(P_zpp_2DbContext context,
                           ILogger<EditModel> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser Pracownik { get; set; }
        public AdministrationRole PracownikRola { get; set; }

        public async Task<IActionResult> OnGetAsync(string? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            Pracownik= await _context.Users.FindAsync(Id);
            //PracownikRola = await _userManager.GetRolesAsync(Id);
            if (Pracownik == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? Id)
        {
            var pracownikToUpdate = await _context.Users.FindAsync(Id);
            var roles = await _userManager.GetRolesAsync(pracownikToUpdate);

            if (pracownikToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<ApplicationUser>(
                pracownikToUpdate,
                "pracownik",

                p => p.FirstName, p => p.LastName, p => p.Rola , p => p.Email))
            {
                await _userManager.RemoveFromRolesAsync(pracownikToUpdate, roles);
                await _userManager.AddToRoleAsync(pracownikToUpdate, PracownikRola.Name);
                await _context.SaveChangesAsync();
                return RedirectToPage("./PracownicyAdmin");
            }

            return Page();
        }
    }
}