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
    public class CompaniesController : Controller
    {
        private readonly P_zpp_2DbContext _context;

        public CompaniesController(P_zpp_2DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Shows list of all existing companies
        /// </summary>
        /// <returns></returns>
        // GET: Companies
        public async Task<IActionResult> Index()
        {
            return View(await _context.company.ToListAsync());
        }

        /// <summary>
        /// Shows parameters of company
        /// </summary>
        /// <param name="id">Id of chosen company</param>
        /// <returns></returns>
        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            CompanyDepartuersListViewModel company = new CompanyDepartuersListViewModel();

            company.companies = await _context.company
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            company.departures = _context.departures.Where(x => x.CompanyID.CompanyId == id).ToList();
          
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates new company
        /// </summary>
        /// <param name="company">Object representing company</param>
        /// <returns></returns>
        // POST: Companies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        /// <summary>
        /// Edits chosen company, basing on its id and object
        /// </summary>
        /// <param name="id">Id of chosen company</param>
        /// <param name="company">Object representing company</param>
        /// <returns></returns>
        // POST: Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName")] Company company)
        {
            if (id != company.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.CompanyId))
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
            return View(company);
        }

        /// <summary>
        /// Shows current data of chosen company
        /// </summary>
        /// <param name="id">Id of chosen company</param>
        /// <returns></returns>
        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _context.company
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        /// <summary>
        /// After pressing delete button, removes company object from database
        /// </summary>
        /// <param name="id">Id of chosen company</param>
        /// <returns></returns>
        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _context.company.FindAsync(id);
            _context.company.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _context.company.Any(e => e.CompanyId == id);
        }
    }
}
