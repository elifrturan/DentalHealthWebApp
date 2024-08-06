using EntityLayer.Concrete;

namespace DentalHealthApp.Models
{
    public class HomeViewModel
    {
        public User User { get; set; }
        public string UserName { get; set; }
        public List<HealthRecord> LastWeekHealthRecords { get; set; }
        public Recommendation RandomRecommendation { get; set; }
        public string NewPassword { get; set; } 
        public string ConfirmPassword { get; set; }
    }
}
