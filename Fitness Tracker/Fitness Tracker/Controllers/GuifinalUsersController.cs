using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fitness_Tracker.Models;

namespace Fitness_Tracker.Controllers
{
    public class GuifinalUsersController : Controller
    {
        private readonly ClassDatabaseContext _context;

        public GuifinalUsersController(ClassDatabaseContext context)
        {
            _context = context;
        }

        // GET: GuifinalUsers
        public async Task<IActionResult> Index()
        {
            var classDatabaseContext = _context.GuifinalUsers.Include(g => g.UserActivityNavigation).Include(g => g.UserGenderNavigation);
            return View(await classDatabaseContext.ToListAsync());
        }

        // GET: GuifinalUsers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.GuifinalUsers == null)
            {
                return NotFound();
            }

            var guifinalUser = await _context.GuifinalUsers
                .Include(g => g.UserActivityNavigation)
                .Include(g => g.UserGenderNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (guifinalUser == null)
            {
                return NotFound();
            }

            return View(guifinalUser);
        }

        // GET: GuifinalUsers/Create
        public IActionResult Create()
        {
            ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId");
            ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId");
            return View();
        }

        // POST: GuifinalUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserStartingWeight,UserCurrentWeight,UserDesiredWeight,UserHeight,UserGender,UserActivity,UserBirthday,UserAge,UserCaloriesToLoseWeight")] GuifinalUser guifinalUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guifinalUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId", guifinalUser.UserActivity);
            ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId", guifinalUser.UserGender);
            return View(guifinalUser);
        }

        // GET: GuifinalUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GuifinalUsers == null)
            {
                return NotFound();
            }

            var guifinalUser = await _context.GuifinalUsers.FindAsync(id);
            if (guifinalUser == null)
            {
                return NotFound();
            }
            ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId", guifinalUser.UserActivity);
            ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId", guifinalUser.UserGender);
            return View(guifinalUser);
        }

        // POST: GuifinalUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,UserStartingWeight,UserCurrentWeight,UserDesiredWeight,UserHeight,UserGender,UserActivity,UserBirthday,UserAge,UserCaloriesToLoseWeight")] GuifinalUser guifinalUser)
        {
            if (id != guifinalUser.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guifinalUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuifinalUserExists(guifinalUser.UserId))
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
            ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId", guifinalUser.UserActivity);
            ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId", guifinalUser.UserGender);
            return View(guifinalUser);
        }

        // GET: GuifinalUsers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.GuifinalUsers == null)
            {
                return NotFound();
            }

            var guifinalUser = await _context.GuifinalUsers
                .Include(g => g.UserActivityNavigation)
                .Include(g => g.UserGenderNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (guifinalUser == null)
            {
                return NotFound();
            }

            return View(guifinalUser);
        }

        // POST: GuifinalUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GuifinalUsers == null)
            {
                return Problem("Entity set 'ClassDatabaseContext.GuifinalUsers'  is null.");
            }
            var guifinalUser = await _context.GuifinalUsers.FindAsync(id);
            if (guifinalUser != null)
            {
                _context.GuifinalUsers.Remove(guifinalUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuifinalUserExists(string id)
        {
          return (_context.GuifinalUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
