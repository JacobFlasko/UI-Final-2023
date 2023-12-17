using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Models
{
    public partial class GuifinalUser
    {
        public string UserId { get; set; }
        public int UserStartingWeight { get; set; }
        public int UserCurrentWeight { get; set; }
        public int UserDesiredWeight { get; set; }
        public int UserHeight { get; set; }
        public int UserGender { get; set; }
        public int UserActivity { get; set; }
        public DateTime UserBirthday { get; set; }
        public int UserAge { get; set; }
        public int UserCaloriesToLoseWeight { get; set; }
        public int UserWeightLost { get; set; }

        public virtual GuifinalActivity UserActivityNavigation { get; set; } = null!;
        public virtual GuifinalGender UserGenderNavigation { get; set; } = null!;
    }
}
