using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashierAlgorithm.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using P_zpp_2.Data;
using P_zpp_2.Models.MyCustomLittleDatabase;
using P_zpp_2.ViewModels;

namespace P_zpp_2.Controllers
{
    public class ScheduleInstructionsController : Controller
    {
        private readonly P_zpp_2DbContext _context;

        public ScheduleInstructionsController(P_zpp_2DbContext context)
        {
            _context = context;
        }

        // GET: ScheduleInstructions
        public async Task<IActionResult> Index()
        {
            var p_zpp_2DbContext = _context.ScheduleInstructions.Include(s => s.Coordinator);
            return View(await p_zpp_2DbContext.ToListAsync());
        }

        // GET: ScheduleInstructions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleInstructions = await _context.ScheduleInstructions
                .Include(s => s.Coordinator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleInstructions == null)
            {
                return NotFound();
            }

            return View(scheduleInstructions);
        }

        // GET: ScheduleInstructions/Create
        public IActionResult Create()
        {
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ScheduleInstructions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleInstructionViewModel scheduleInstructionViewModel)
        {
            ScheduleInstructions scheduleInstructions = new ScheduleInstructions();
            scheduleInstructions.Name = scheduleInstructionViewModel.scheduleInstructions.Name;
            ShiftInfoForScheduleGenerating shiftOne = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftTwo = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftThree = new ShiftInfoForScheduleGenerating();
            shiftOne.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstructionViewModel.startOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftOne.ShiftSetEndTime = DateTime.ParseExact(scheduleInstructionViewModel.endOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstructionViewModel.startTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetEndTime = DateTime.ParseExact(scheduleInstructionViewModel.endTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstructionViewModel.startThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetEndTime = DateTime.ParseExact(scheduleInstructionViewModel.endThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            if(scheduleInstructionViewModel.długość_zmiany_w_dniach != null)
            {
                shiftOne.ShiftLengthInDays = (int)scheduleInstructionViewModel.długość_zmiany_w_dniach;
                shiftTwo.ShiftLengthInDays = (int)scheduleInstructionViewModel.długość_zmiany_w_dniach;
                shiftThree.ShiftLengthInDays = (int)scheduleInstructionViewModel.długość_zmiany_w_dniach;
            }
            List<ShiftInfoForScheduleGenerating> listOfShifts = new List<ShiftInfoForScheduleGenerating>();
            listOfShifts.Add(shiftOne);
            listOfShifts.Add(shiftTwo);
            listOfShifts.Add(shiftThree);
            scheduleInstructions.ListOfShistsInJSON = JsonSerializer.Serialize(listOfShifts);
            if (ModelState.IsValid)
            {
                _context.Add(scheduleInstructions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id", scheduleInstructions.CoordinatorId);
            return View(scheduleInstructions);
        }

        // GET: ScheduleInstructions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleInstructions = await _context.ScheduleInstructions.FindAsync(id);
            if (scheduleInstructions == null)
            {
                return NotFound();
            }
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id", scheduleInstructions.CoordinatorId);
            return View(scheduleInstructions);
        }

        // POST: ScheduleInstructions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ListOfShistsInJSON,CoordinatorId")] ScheduleInstructions scheduleInstructions)
        {
            if (id != scheduleInstructions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleInstructions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleInstructionsExists(scheduleInstructions.Id))
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
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id", scheduleInstructions.CoordinatorId);
            return View(scheduleInstructions);
        }

        // GET: ScheduleInstructions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleInstructions = await _context.ScheduleInstructions
                .Include(s => s.Coordinator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduleInstructions == null)
            {
                return NotFound();
            }

            return View(scheduleInstructions);
        }

        // POST: ScheduleInstructions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleInstructions = await _context.ScheduleInstructions.FindAsync(id);
            _context.ScheduleInstructions.Remove(scheduleInstructions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult ChooseAlgorithm()
        {
            return View();
        }

        private bool ScheduleInstructionsExists(int id)
        {
            return _context.ScheduleInstructions.Any(e => e.Id == id);
        }
    }
}
