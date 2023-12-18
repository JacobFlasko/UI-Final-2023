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

        public async Task<IActionResult> Index()
        {
            GuifinalUser user = new GuifinalUser();
            if(User.Identity.IsAuthenticated)
            {
                user = await _context.GuifinalUsers.FindAsync(User.Identity.Name);
                if(user != null)
                {
                    int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
                    ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference) * 100;
                    ViewData["LostWrittenPercent"] = (Math.Truncate((((double)user.UserWeightLost / weightDifference) * 100) * 100) / 100);


                    _viewModel.DailyCalorieGoal = user.UserCaloriesToLoseWeight;
                    _viewModel.CaloriesLeft = user.UserCaloriesToLoseWeight;

                    ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
                    ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;
                    ViewData["CaloriePercent"] = ((double)_viewModel.CaloriesLeft / _viewModel.DailyCalorieGoal) * 100;

                    var percentEaten = ((double)_viewModel.CaloriesConsumed / _viewModel.DailyCalorieGoal) * 100;
                    ViewData["CaloriesWrittenPercent"] = (Math.Truncate(percentEaten * 100) / 100);

                    ViewData["Birthday"] = user.UserBirthday.Month + "/" + user.UserBirthday.Day + "/" + user.UserBirthday.Year;
                    ViewData["Gender"] = _context.GuifinalGenders.FindAsync(user.UserGender).Result.GenderName ;
                    ViewData["Activity"] = _context.GuifinalActivities.FindAsync(user.UserActivity).Result.ActivityDescriptor;
                }
                

            }
            else
            {
                //user.UserId = User.Identity.Name;
            }

            return View("Index", user);
        }

        [HttpPost]
        public async Task<IActionResult> RecordFood(string foodName, int foodCalories)
        {
            // Record the food and update the view model
            /*_viewModel.FoodName = foodName;
            _viewModel.FoodCalories = foodCalories;
            _viewModel.CurrentCaloriesConsumed += foodCalories;
            _viewModel.CaloriesLeft -= foodCalories;*/

            GuifinalUser user = new GuifinalUser();
            user = await _context.GuifinalUsers.FindAsync(User.Identity.Name);
            int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
            ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference) * 100;
            ViewData["LostWrittenPercent"] = (Math.Truncate((((double)user.UserWeightLost / weightDifference) * 100) * 100) / 100);
            _viewModel.CaloriesLeft -= foodCalories;
            _viewModel.CaloriesConsumed += foodCalories;

            _viewModel.DailyCalorieGoal = user.UserCaloriesToLoseWeight;
            ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
            ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;
            ViewData["CaloriePercent"] = ((double)_viewModel.CaloriesLeft / _viewModel.DailyCalorieGoal) * 100;

            var percentEaten = ((double)_viewModel.CaloriesConsumed / _viewModel.DailyCalorieGoal) * 100;
            ViewData["CaloriesWrittenPercent"] = (Math.Truncate(percentEaten * 100) / 100);

            ViewData["Birthday"] = user.UserBirthday.Month + "/" + user.UserBirthday.Day + "/" + user.UserBirthday.Year;
            ViewData["Gender"] = _context.GuifinalGenders.FindAsync(user.UserGender).Result.GenderName;

            //ViewData["LostWeight"] = lostWeight;

            return View("Index", user);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int weightLost)
        {
            GuifinalUser user = new GuifinalUser();
            if (User.Identity.IsAuthenticated)
            {
                user = await _context.GuifinalUsers.FindAsync(User.Identity.Name);
                user.UserWeightLost = weightLost;
                user.UserCurrentWeight = user.UserStartingWeight - weightLost;
                int weightDifference = user.UserStartingWeight - user.UserDesiredWeight;
                ViewData["LostPercent"] = ((double)user.UserWeightLost / weightDifference)*100;
                ViewData["LostWrittenPercent"] = (Math.Truncate((((double)user.UserWeightLost / weightDifference) * 100) * 100) / 100);

                ViewData["CaloriesLeft"] = _viewModel.CaloriesLeft;
                ViewData["CaloriesConsumed"] = _viewModel.CaloriesConsumed;
                ViewData["CaloriePercent"] = ((double)_viewModel.CaloriesLeft / _viewModel.DailyCalorieGoal) * 100;

                var percentEaten = ((double)_viewModel.CaloriesConsumed / _viewModel.DailyCalorieGoal) *100;
                ViewData["CaloriesWrittenPercent"] = (Math.Truncate(percentEaten * 100) / 100);

                ViewData["Birthday"] = user.UserBirthday.Month + "/" + user.UserBirthday.Day + "/" + user.UserBirthday.Year;
                ViewData["Gender"] = _context.GuifinalGenders.FindAsync(user.UserGender).Result.GenderName;


                await _context.SaveChangesAsync();

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

            if(user != null)
            {
                ViewBag.UserGender = new SelectList(_context.GuifinalGenders, "GenderId", "GenderName", user.UserGender);
                ViewBag.UserActivity = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityDescriptor", user.UserActivity);
                return View(user);
            }
            else
            {
                ViewBag.UserGender = new SelectList(_context.GuifinalGenders, "GenderId", "GenderName");
                ViewBag.UserActivity = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityDescriptor");
                return View();
            }

            
        }

        [HttpPost]
        public async Task<IActionResult> EditInfo(int startingWeight, int currentWeight, int desiredWeight, int HeightInInches, int gender, int activity, DateTime birthday, int ctlw)
        {
            var user = await _context.GuifinalUsers.FindAsync(User.Identity.Name);
            bool isNew = false;

            if(user == null)
            {
                user = new GuifinalUser();
                user.UserId = User.Identity.Name;
                isNew = true;
            }

            user.UserStartingWeight = startingWeight;
            user.UserCurrentWeight = currentWeight;
            user.UserDesiredWeight = desiredWeight;

            user.UserWeightLost = user.UserStartingWeight - user.UserCurrentWeight;

            user.UserHeight = HeightInInches;
            user.UserGender = gender;
            user.UserActivity = activity;
            user.UserBirthday = birthday;

            user.UserAge = (int)((DateTime.Now - user.UserBirthday).TotalDays/365);

            user.UserCaloriesToLoseWeight = ctlw;

            if(isNew)
            {
                _context.GuifinalUsers.Add(user);
            }
            await _context.SaveChangesAsync();


            ViewBag.UserGender = new SelectList(_context.GuifinalGenders, "GenderId", "GenderName", user.UserGender);
            ViewBag.UserActivity = new SelectList(_context.GuifinalActivities, "ActivityId", "ActivityDescriptor", user.UserActivity);

            return View("EditInfo", user);
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
