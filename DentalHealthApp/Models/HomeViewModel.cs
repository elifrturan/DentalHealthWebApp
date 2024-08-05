using EntityLayer.Concrete;

namespace DentalHealthApp.Models
{
    public class HomeViewModel
    {
        public string UserName { get; set; }
        public List<HealthRecord> LastWeekHealthRecords { get; set; }
        public Recommendation RandomRecommendation { get; set; }
    }
}
