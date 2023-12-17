using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fitness_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ClassDatabaseContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ClassDatabaseContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Info()
        //{
        //    return View();
        //}

        public IActionResult Info(int user)
        {
            ViewData["currentUser"] = user;
            return View();
            var details = from b in _context.GuifinalUsers select b;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Info([Bind("UserId,UserStartingWeight,UserCurrentWeight,UserDesiredWeight,UserHeight,UserGender,UserActivity,UserBirthday,UserAge,UserCaloriesToLoseWeight")] GuifinalUser guifinalUser)
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

        public async Task<IActionResult> EditInfo(int? id)
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
            //ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId", guifinalUser.UserActivity);
            //ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId", guifinalUser.UserGender);
            return View(guifinalUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(int id, [Bind("UserId,UserStartingWeight,UserCurrentWeight,UserDesiredWeight,UserHeight,UserGender,UserActivity,UserBirthday,UserAge,UserCaloriesToLoseWeight")] GuifinalUser guifinalUser)
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

        private bool GuifinalUserExists(int id)
        {
            return (_context.GuifinalUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}