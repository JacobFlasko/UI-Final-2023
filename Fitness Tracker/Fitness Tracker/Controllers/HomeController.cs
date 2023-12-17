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

        public HomeController(ILogger<HomeController> logger)
        {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}