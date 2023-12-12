using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Models
{
    public partial class GuifinalGender
    {
        public GuifinalGender()
        {
            GuifinalUsers = new HashSet<GuifinalUser>();
        }

        public int GenderId { get; set; }
        public string GenderName { get; set; } = null!;

        public virtual ICollection<GuifinalUser> GuifinalUsers { get; set; }
    }
}
