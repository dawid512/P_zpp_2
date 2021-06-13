using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P_zpp_2.Data;
using P_zpp_2.Models;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ViewModels;

namespace P_zpp_2.Controllers
{
    public class LeavesAdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly P_zpp_2DbContext _context;
        private ICollection<Departures> _departures;
        private ICollection<ApplicationUser> _applicationUser;

        public LeavesAdminController(P_zpp_2DbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
            _departures = _context.departures.ToList();
            _applicationUser = context.Users.ToList();

        }

        // GET: LeavesAdmin
        public async Task<IActionResult> Index()
        {
            var ActualLoggedSupervisor = await _userManager.GetUserAsync(User);

            var user = await _userManager.GetUserAsync(User);
            var userid =  _userManager.GetUserId(User);
            //new SelectList(_departures, "DeprtureId", "DepartureName");
            var userDepartment = _applicationUser.Where(x => x.Id == userid).Select(x => x.DeptId);
            LeavesPracownicyListViewModel leavesPracownicyListViewModel = new LeavesPracownicyListViewModel();
           // .Where(x => x.Idusera.DeptId == de)
           
            leavesPracownicyListViewModel.departure = _departures.Where(d => d.DeprtureId == user.DeptId).ToList();

            //_context.departures.Where(d=>d.User_id == user).Select(d =>
            //                                    new SelectListItem
            //                                    {
            //                                        Value = d.DeprtureId.ToString(),
            //                                        Text = d.DepartureName
            //                                    });
            leavesPracownicyListViewModel.Pracownicy = _applicationUser.Select(a =>
                                              new SelectListItem
                                              {
                                                  Value = a.Id,
                                                  Text = a.FirstName + " " + a.LastName
                                              });
            var currentUserDepartureId = _departures.Where(d => d.DeprtureId == user.DeptId).Select(X=>X.DeprtureId).FirstOrDefault();
        
            leavesPracownicyListViewModel.leaves = await _context.leaves.Where(X=>X.Iddepartuers.DeprtureId== currentUserDepartureId).ToListAsync();
            return View(leavesPracownicyListViewModel);

      
        }

        // GET: LeavesAdmin/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int? id, [Bind("Id,CheckIn,CheckOut,Leavesname,Status,Status_zaakceptopwane,Status_odrzucone")] Leaves leaves)
        {
            if (id != leaves.Id)
            {
                return NotFound();
            }

            if (leaves.Status_odrzucone == false && leaves.Status_zaakceptopwane == false)
            {
                leaves.Status = "Oczekujące";
            }
            else if (leaves.Status_odrzucone == true)
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

        // GET: LeavesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeavesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CheckIn,CheckOut,Leavesname,Status,Status_zaakceptopwane,Status_odrzucone")] Leaves leaves)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaves);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaves);
        }

        // GET: LeavesAdmin/Edit/5
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

        // POST: LeavesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CheckIn,CheckOut,Leavesname,Status,Status_zaakceptopwane,Status_odrzucone")] Leaves leaves)
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

        // GET: LeavesAdmin/Delete/5
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

        // POST: LeavesAdmin/Delete/5
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
