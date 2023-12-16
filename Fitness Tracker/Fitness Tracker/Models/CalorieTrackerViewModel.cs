namespace Fitness_Tracker.Models
{
    public class CalorieTrackerViewModel
    {
        public string? FoodName { get; set; }
        public int FoodCalories { get; set; }
        public int DailyCalorieGoal { get; set; }
        public int CurrentCaloriesConsumed { get; set; }
        public int CaloriesLeft { get; set; }
        public double WeightLost { get; set; }
    }

}
