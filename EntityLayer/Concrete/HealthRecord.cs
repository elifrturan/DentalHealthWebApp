﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class HealthRecord
    {
        [Key]
        public int RecordID { get; set; }
        public string RecordDescription { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime RecordTime { get; set; }
        public string RecordDuration { get; set; }
        public bool IsApplied { get; set; }

        public int GoalID { get; set; }
        public HealthGoal Goal { get; set; }
    }
}
