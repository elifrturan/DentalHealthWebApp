using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class HealthGoal
    {
        [Key]
        public int GoalID { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public string GoalPeriod { get; set; }
        public string GoalPriority { get; set; }
        public DateTime GoalCreateDate { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }


    }
}
