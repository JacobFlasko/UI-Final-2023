using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> EditInfo()
        {
            //GuifinalUser user = new GuifinalUser();
            var user = await _context.GuifinalUsers.FindAsync(User.Identity.Name);

            ViewBag.UserGender = new SelectList(_context.GuifinalGenders, "GenderId", "GenderName", user.UserGender);
            ViewBag.UserActivity = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityDescriptor", user.UserActivity);

            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
