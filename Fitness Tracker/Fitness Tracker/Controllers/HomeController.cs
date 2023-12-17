using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
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
            CurrentCaloriesConsumed = 0,
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
                ViewData["LostPercent"] = (double)user.UserWeightLost/ weightDifference;
            }
            else
            {
                //user.UserId = User.Identity.Name;
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult RecordFood(string foodName, int foodCalories)
        {
            // Record the food and update the view model
            _viewModel.FoodName = foodName;
            _viewModel.FoodCalories = foodCalories;
            _viewModel.CurrentCaloriesConsumed += foodCalories;
            _viewModel.CaloriesLeft -= foodCalories;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult RecordWeightLost(GuifinalUser user)
        {
            if (User.Identity.IsAuthenticated)
            {
                //user.UserCurrentWeight -= weightLost;
                int lostWeight = user.UserStartingWeight - user.UserCurrentWeight;
                int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
                ViewData["LostPercent"] = (double)lostWeight / weightDifference;
                ViewData["LostWeight"] = lostWeight;
            }
            else
            {
                //user.UserId = User.Identity.Name;
            }

            // Record the weight lost and update the view model


            return RedirectToAction("Index", user);
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