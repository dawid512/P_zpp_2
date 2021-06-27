using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P_zpp_2.Models.MyCustomLittleDatabase;
using Microsoft.AspNetCore.Identity;

namespace P_zpp_2.Controllers
{
    public class LeavesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly P_zpp_2DbContext _context;
        private ICollection<Departures> _departures;
        private ICollection<ApplicationUser> _applicationUser;

        public LeavesController(P_zpp_2DbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _departures = _context.departures.ToList();
            _applicationUser = context.Users.ToList();
        }

      

        // GET: Leaves
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userid = _userManager.GetUserId(User);

            LeavesPracownicyListViewModel leavesPracownicyListViewModel = new LeavesPracownicyListViewModel();
            leavesPracownicyListViewModel.leaves = _context.leaves.Where(x=>x.Idusera==user).ToList();
            leavesPracownicyListViewModel.singleDep = _departures.Where(x => x.DeprtureId == user.DeptId).FirstOrDefault();

            leavesPracownicyListViewModel.workers = _applicationUser.Select(a =>
                                              new SelectListItem
                                              {
                                                  Value = a.Id,
                                                  Text = a.FirstName + " " + a.LastName
                                              });


            return View(leavesPracownicyListViewModel);
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.leaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Leaves/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Leaves/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeavesPracownicyListViewModel leavesPracownicyListViewModel)
        {
            Leaves leaves = leavesPracownicyListViewModel;

            var user = await _userManager.GetUserAsync(User);

            leaves.Iddepartuers = _departures.Where(x => x.DeprtureId == user.DeptId).FirstOrDefault();
       
            leaves.Idusera = await _userManager.GetUserAsync(User);
            if (leaves.Status_odrzucone == false && leaves.Status_zaakceptopwane == false)
            {
                leaves.Status = "Oczekujące";
            }
            else if (leaves.Status_odrzucone == true )
            {
                leaves.Status_zaakceptopwane = false;
                leaves.Status = "Odrzucony";
            }
            else if (leaves.Status_zaakceptopwane == true)
            {
                leaves.Status_odrzucone = false;
                leaves.Status = "Przyznany";
            }

            if (ModelState.IsValid)
            {
                _context.Add(leaves);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaves = await _context.leaves.FindAsync(id);
            if (leaves == null)
            {
                return NotFound();
            }
            return View(leaves);
        }

        // POST: Leaves/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckIn,CheckOut,Leavesname,Status")] Leaves leaves)
        {
            if (id != leaves.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaves);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeavesExists(leaves.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaves);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaves = await _context.leaves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaves == null)
            {
                return NotFound();
            }

            return View(leaves);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaves = await _context.leaves.FindAsync(id);
            _context.leaves.Remove(leaves);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeavesExists(int id)
        {
            return _context.leaves.Any(e => e.Id == id);
        }
    }
}
