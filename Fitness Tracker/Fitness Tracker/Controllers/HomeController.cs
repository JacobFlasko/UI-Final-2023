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

        private static CalorieTrackerViewModel _viewModel = new CalorieTrackerViewModel
        {
            DailyCalorieGoal = 2000, // Set your daily calorie goal
            CaloriesConsumed = 0,
            CaloriesLeft = 2000, // Initialize to the daily goal
            WeightLost = 0
        };

        public IActionResult Index()
        {
            GuifinalUser user = new GuifinalUser();
            if(User.Identity.IsAuthenticated)
            {
                var temp = _context.GuifinalUsers.FindAsync(User.Identity.Name);
                user = temp.Result;
                int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
                ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference) * 100;

                _viewModel.DailyCalorieGoal = user.UserCaloriesToLoseWeight;
                _viewModel.CaloriesLeft = user.UserCaloriesToLoseWeight;
                ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
                ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;
                
            }
            else
            {
                //user.UserId = User.Identity.Name;
            }

            return View("Index", user);
        }

        [HttpPost]
        public IActionResult RecordFood(string foodName, int foodCalories)
        {
            // Record the food and update the view model
            /*_viewModel.FoodName = foodName;
            _viewModel.FoodCalories = foodCalories;
            _viewModel.CurrentCaloriesConsumed += foodCalories;
            _viewModel.CaloriesLeft -= foodCalories;*/

            GuifinalUser user = new GuifinalUser();
            var temp = _context.GuifinalUsers.FindAsync(User.Identity.Name);
            user = temp.Result;
            int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
            ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference) * 100;
            _viewModel.CaloriesLeft -= foodCalories;
            _viewModel.CaloriesConsumed += foodCalories;

            ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
            ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;

            //ViewData["LostWeight"] = lostWeight;

            return View("Index", user);
        }

        [HttpPost]
        public IActionResult Index(int weightLost)
        {
            GuifinalUser user = new GuifinalUser();
            if (User.Identity.IsAuthenticated)
            {
                var temp = _context.GuifinalUsers.FindAsync(User.Identity.Name);
                user = temp.Result;
                user.UserWeightLost = weightLost;
                user.UserCurrentWeight = user.UserStartingWeight - weightLost;
                int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
                ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference)*100;

                ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
                ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;

            }
            else
            {
                //user.UserId = User.Identity.Name;
            }

            // Record the weight lost and update the view model


            return View("Index", user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

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
        public async Task<IActionResult> EditInfo(/*int id,*/ [Bind("UserId,UserStartingWeight,UserCurrentWeight,UserDesiredWeight,UserHeight,UserGender,UserActivity,UserBirthday,UserAge,UserCaloriesToLoseWeight")] GuifinalUser user)
        {
            /*if (id != guifinalUser.UserId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (user == null)
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
            ViewData["UserActivity"] = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityId", user.UserActivity);
            ViewData["UserGender"] = new SelectList(_context.GuifinalGenders, "GenderId", "GenderId", user.UserGender);
            return View(user);
        }

        /*private bool GuifinalUserExists(int id)
        {
            return (_context.GuifinalUsers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}