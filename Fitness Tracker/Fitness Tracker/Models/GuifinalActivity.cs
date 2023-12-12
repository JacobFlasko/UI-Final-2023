using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Models
{
    public partial class GuifinalActivity
    {
        public GuifinalActivity()
        {
            GuifinalUsers = new HashSet<GuifinalUser>();
        }

        public int ActivityId { get; set; }
        public string ActivityDescriptor { get; set; } = null!;

        public virtual ICollection<GuifinalUser> GuifinalUsers { get; set; }
    }
}
