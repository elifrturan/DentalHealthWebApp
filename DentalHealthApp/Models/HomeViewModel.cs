using EntityLayer.Concrete;

namespace DentalHealthApp.Models
{
    public class HomeViewModel
    {
        public User User { get; set; }
        public string UserName { get; set; }
        public List<HealthRecord> LastWeekHealthRecords { get; set; }
        public List<HealthGoal> LastWeekHealthGoals { get; set; }
        public Recommendation RandomRecommendation { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public List<HealthRecord> HealthRecords { get; set; }
        public List<Note> Notes { get; set; }
        public Note NewNote { get; set; }
        public int NoteID { get; set; }
        public string NoteTitle { get; set; }
        public string NoteDescription { get; set; }
        public string NoteImage { get; set; }
    }
}
