﻿using System;
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
using Microsoft.AspNetCore.Identity;
using P_zpp_2.Models;
using P_zpp_2.ScheduleAlgoritms.FourBrigadeSystemAlgorithm;

namespace P_zpp_2.Controllers
{
    public class ScheduleInstructionsController : Controller
    {
        private readonly P_zpp_2DbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ScheduleInstructionsController(P_zpp_2DbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ScheduleInstructions
        public async Task<IActionResult> Index( string? algorithmName)
        {
            var scheduleInstructions = await _context.ScheduleInstructions.ToListAsync();
            //await scheduleInstructions;


            return View(Tuple.Create( scheduleInstructions, algorithmName));
        }

        /// <summary>
        /// Shows details of selected instruction for algoritm
        /// </summary>
        /// <param name="id">Id of selected instruction</param>
        /// <returns></returns>
        // GET: ScheduleInstructions/Details/5
        public async Task<IActionResult> Details(int? id )
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleInstructions = await _context.ScheduleInstructions
                .Include(s => s.Coordinator)
                .FirstOrDefaultAsync(m => m.Id == id);
            var shiftInfo = JsonSerializer.Deserialize<List<ShiftInfoForScheduleGenerating>>(scheduleInstructions.ListOfShistsInJSON);
            scheduleInstructions.ListOfShistsInJSON = "";
            foreach(var item in shiftInfo)
            {
                scheduleInstructions.ListOfShistsInJSON += String.Format("{0} - {1}\n",item.ShiftSetBeginTime.TimeOfDay.ToString(), item.ShiftSetEndTime.TimeOfDay.ToString());
            }

            if (scheduleInstructions == null)
            {
                return NotFound();
            }

            return View(scheduleInstructions);
        }

        /// <summary>
        /// Creates view for creating instruction for algorithm
        /// </summary>
        /// <param name="algorithmName">Name of algorithm</param>
        /// <returns></returns>
        // GET: ScheduleInstructions/Create
        public IActionResult Create(string? algorithmName)
        {
            ViewBag.algorithmName = algorithmName;
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        /// <summary>
        /// Creates algorithm instructions with ScheduleInstructionViewModel with json info
        /// </summary>
        /// <param name="tuple">Contains scheduleInstructions, shift length and days for this shift</param>
        /// <returns></returns>
        // POST: ScheduleInstructions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ScheduleInstructionViewModel tuple)
        {
            ScheduleInstructions scheduleInstructions = new ScheduleInstructions();
            scheduleInstructions.Name = tuple.scheduleInstructions.Name;
            ShiftInfoForScheduleGenerating shiftOne = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftTwo = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftThree = new ShiftInfoForScheduleGenerating();
            shiftOne.ShiftSetBeginTime = DateTime.ParseExact(tuple.startOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftOne.ShiftSetEndTime = DateTime.ParseExact(tuple.endOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetBeginTime = DateTime.ParseExact(tuple.startTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetEndTime = DateTime.ParseExact(tuple.endTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetBeginTime = DateTime.ParseExact(tuple.startThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetEndTime = DateTime.ParseExact(tuple.endThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftOne.ShiftLengthInDays = (int)tuple.ShiftLengthInDays;
            shiftTwo.ShiftLengthInDays = (int)tuple.ShiftLengthInDays;
            shiftThree.ShiftLengthInDays = (int)tuple.ShiftLengthInDays;
            List<ShiftInfoForScheduleGenerating> listOfShifts = new List<ShiftInfoForScheduleGenerating>();
            listOfShifts.Add(shiftOne);
            listOfShifts.Add(shiftTwo);
            listOfShifts.Add(shiftThree);
            var coordinator = await _userManager.GetUserAsync(User);
            scheduleInstructions.CoordinatorId = coordinator.Id;
            foreach(var item in listOfShifts)
            {
                if (item.ShiftSetBeginTime > item.ShiftSetEndTime)
                {
                    item.IsOvernight = true;
                }
                else
                {
                    item.IsOvernight = false;
                }
            }
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

        /// <summary>
        /// Creates view for edition of instructions
        /// </summary>
        /// <param name="id">Id of chosen instructions</param>
        /// <param name="algorithmName">Algorithm name</param>
        /// <returns></returns>
        // GET: ScheduleInstructions/Edit/5
        public async Task<IActionResult> Edit(int? id, string? algorithmName)
        {
            if (id == null)
            {
                return NotFound();
            }
            ScheduleInstructionViewModel schedule = new ScheduleInstructionViewModel();
            schedule.scheduleInstructions = await _context.ScheduleInstructions.FindAsync(id);
            if (schedule.scheduleInstructions == null)
            {
                return NotFound();
            }
            ViewBag.algorithmName = algorithmName;
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id", schedule.scheduleInstructions.CoordinatorId);
            return View(schedule);
        }

        /// <summary>
        /// Updates chosen instructions for schedule
        /// </summary>
        /// <param name="id">Id of chosen instructions</param>
        /// <param name="scheduleInstr">Contains scheduleInstructions, shift length and days for this shift</param>
        /// <param name="algorithmName">Name of instructions for schedule</param>
        /// <returns></returns>
        // POST: ScheduleInstructions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Name")]*/ ScheduleInstructionViewModel scheduleInstr, string? algorithmName)
        {
            if (id != scheduleInstr.Id)
            {
                return NotFound();
            }

            scheduleInstr.Name = scheduleInstr.scheduleInstructions.Name;
            ShiftInfoForScheduleGenerating shiftOne = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftTwo = new ShiftInfoForScheduleGenerating();
            ShiftInfoForScheduleGenerating shiftThree = new ShiftInfoForScheduleGenerating();
            shiftOne.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstr.startOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftOne.ShiftSetEndTime = DateTime.ParseExact(scheduleInstr.endOne, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstr.startTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftTwo.ShiftSetEndTime = DateTime.ParseExact(scheduleInstr.endTwo, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetBeginTime = DateTime.ParseExact(scheduleInstr.startThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            shiftThree.ShiftSetEndTime = DateTime.ParseExact(scheduleInstr.endThree, "H:mm", null, System.Globalization.DateTimeStyles.None);
            if (scheduleInstr.ShiftLengthInDays != null)
            {
                shiftOne.ShiftLengthInDays = (int)scheduleInstr.ShiftLengthInDays;
                shiftTwo.ShiftLengthInDays = (int)scheduleInstr.ShiftLengthInDays;
                shiftThree.ShiftLengthInDays = (int)scheduleInstr.ShiftLengthInDays;
            }
            List<ShiftInfoForScheduleGenerating> listOfShifts = new List<ShiftInfoForScheduleGenerating>();
            listOfShifts.Add(shiftOne);
            listOfShifts.Add(shiftTwo);
            listOfShifts.Add(shiftThree);
            var coordinator = await _userManager.GetUserAsync(User);
            scheduleInstr.CoordinatorId = coordinator.Id;
            foreach (var item in listOfShifts)
            {
                if (item.ShiftSetBeginTime > item.ShiftSetEndTime)
                {
                    item.IsOvernight = true;
                }
                else
                {
                    item.IsOvernight = false;
                }
            }
            scheduleInstr.ListOfShistsInJSON = JsonSerializer.Serialize(listOfShifts);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleInstr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleInstructionsExists(scheduleInstr.Id))
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
            ViewData["CoordinatorId"] = new SelectList(_context.Users, "Id", "Id", scheduleInstr.CoordinatorId);
            return View( scheduleInstr);
        }

        /// <summary>
        /// Shows details of instruction set to be deleted and confirmation button
        /// </summary>
        /// <param name="id">Id of instruction set</param>
        /// <returns></returns>
        // GET: ScheduleInstructions/Delete/5
        public async Task<IActionResult> Delete(int? id )
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

        /// <summary>
        /// Deletes chosen instruction set
        /// </summary>
        /// <param name="id">Id of removed instruction set</param>
        /// <returns></returns>
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
