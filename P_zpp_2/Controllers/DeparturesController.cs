﻿
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

        public DeparturesController(P_zpp_2DbContext context)
        {
            _context = context;
        }

        // GET: Departures
        public async Task<IActionResult> Index()
        {

            return View(await _context.departures.Include("CompanyID").ToListAsync());
        }

        // GET: Departures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ForDeparturesViewModel departures = new ForDeparturesViewModel();
            departures.departures = await _context.departures.Include("CompanyID")
            .FirstOrDefaultAsync(m => m.DeprtureId == id);
            departures.appUsers = await _context.Users.Where(x => x.DeptId == departures.departures.DeprtureId).ToListAsync();

            if (departures == null)
            {
                return NotFound();
            }


            return View(departures);
        }

        // GET: Departures/Create
        public IActionResult Create()
        {
            var deps = _context.company.Select(x => x);
            var model = new CompanyDepartuersListViewModel();

            model.companyList = new SelectList(deps, "CompanyId", "CompanyName");



            return View(model);

        }

        // POST: Departures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeprtureId,CompanyID,DepartureName")] Departures departures)
        {
            string str = Request.Form["Companies"].ToString();
            departures.CompanyID = _context.company.Where(x => x.CompanyId.ToString() == str).FirstOrDefault();
            if (ModelState.IsValid)
            {
                _context.Add(departures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Departures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            /*if (id == null)
            {
                return NotFound();
            }

            var departures = await _context.departures.FindAsync(id);
            if (departures == null)
            {
                return NotFound();
            }*/
            var deps = await _context.departures.FindAsync(id);
            var comps = _context.company.Select(x => x);
            var model = new CompanyDepartuersListViewModel();
            model.DeprtureId = deps.DeprtureId;
            model.companyList = new SelectList(comps, "CompanyId", "CompanyName");

            return View(model);
        }

        // POST: Departures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeprtureId,CompanyID,DepartureName")]  CompanyDepartuersListViewModel deps)
        {

            if (id != deps.DeprtureId)
            {
                return NotFound();
            }
            string str = Request.Form["Companies"].ToString();

            deps.CompanyID = _context.company.Where(x => x.CompanyId.ToString() == str).FirstOrDefault();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deps);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeparturesExists(deps.DeprtureId))
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
            return View(deps);
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