using Fitness_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Fitness_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
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
            return View(_viewModel);
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

        [HttpPost]
        public IActionResult RecordWeightLost(double weightLost)
        {
            // Record the weight lost and update the view model
            _viewModel.WeightLost += weightLost;

            return RedirectToAction("Index");
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