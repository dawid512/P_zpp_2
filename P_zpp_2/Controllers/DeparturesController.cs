using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P_zpp_2.Data;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ViewModels;

namespace P_zpp_2.Controllers
{
    public class DeparturesController : Controller
    {
        private readonly P_zpp_2DbContext _context;
        private ICollection<Company> _companies;

        public DeparturesController(P_zpp_2DbContext context)
        {
            _context = context;
            _companies = _context.company.ToList();
        }

        // GET: Departures
        public async Task<IActionResult> Index()
        {
            CompanyDepartuersListViewModel companyDepartuersListViewModel = new CompanyDepartuersListViewModel();
            companyDepartuersListViewModel.company = new SelectList(_companies, "Id", "Name");

            return View(await _context.departures.ToListAsync());
        }

        // GET: Departures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departures = await _context.departures
                .FirstOrDefaultAsync(m => m.DeprtureId == id);
            if (departures == null)
            {
                return NotFound();
            }

            return View(departures);
        }

        // GET: Departures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyDepartuersListViewModel companyDepartuersListViewModel)
        {
        
          
            Departures departures = companyDepartuersListViewModel;
            
           
            departures.CompanyID = _companies.FirstOrDefault(x => x.CompanyId == companyDepartuersListViewModel.idcompany);


            if (ModelState.IsValid)
            {
                _context.Add(departures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departures);
        }

        // GET: Departures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departures = await _context.departures.FindAsync(id);
            if (departures == null)
            {
                return NotFound();
            }
            return View(departures);
        }

        // POST: Departures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeprtureId,Shifts,DepartureName")] Departures departures)
        {
            if (id != departures.DeprtureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departures);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeparturesExists(departures.DeprtureId))
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
            return View(departures);
        }

        // GET: Departures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departures = await _context.departures
                .FirstOrDefaultAsync(m => m.DeprtureId == id);
            if (departures == null)
            {
                return NotFound();
            }

            return View(departures);
        }

        // POST: Departures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departures = await _context.departures.FindAsync(id);
            _context.departures.Remove(departures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeparturesExists(int id)
        {
            return _context.departures.Any(e => e.DeprtureId == id);
        }
    }
}
