using BusinessLayer.Abstract;
using DentalHealthApp.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DentalHealthApp.Controllers
{
    [Authorize]
    public class HealthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHealthGoalService _healthGoalService;
        private readonly IHealthRecordService _healthRecordService;
        private readonly IRecommendationService _recommendationService;
        private readonly INoteService _noteService;

        public HealthController(IUserService userService, IHealthGoalService healthGoalService, IHealthRecordService healthRecordService, IRecommendationService recommendationService, INoteService noteService)
        {
            _userService = userService;
            _healthGoalService = healthGoalService;
            _healthRecordService = healthRecordService;
            _recommendationService = recommendationService;
            _noteService = noteService;
        }
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lastWeekHealthGoals = await _healthGoalService.GetHealthGoalsForLast7DaysAsync(user.UserID);
            var recommendation = await _recommendationService.GetRandomRecommendationAsync();
             var healthRecords = await _healthRecordService.GetAllByUserIdAsync(user.UserID);
            var notes = await _noteService.GetAllByUserIdAsync(user.UserID);

            var model = new HomeViewModel
            {
                UserName = user.UserFullName,
                LastWeekHealthGoals = lastWeekHealthGoals,
                RandomRecommendation = recommendation,
                HealthRecords = healthRecords,
                Notes = notes,
                NewNote = new Note(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHealthRecord(int SelectedRecordId, DateTime? RecordDate, int? RecordDuration, string DurationType, bool? IsApplied)
        {
            var healthRecord = await _healthRecordService.GetAsync(SelectedRecordId);

            if (healthRecord == null)
            {
                return NotFound();
            }

            if (RecordDate.HasValue)
            {
                healthRecord.RecordDate = RecordDate.Value;
            }

            if (RecordDuration.HasValue && !string.IsNullOrEmpty(DurationType))
            {
                healthRecord.RecordDuration = $"{RecordDuration.Value} {DurationType}";
            }

            if (IsApplied.HasValue)
            {
                healthRecord.IsApplied = IsApplied.Value;
            }

            await _healthRecordService.UpdateAsync(healthRecord);

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(string noteTitle, string noteDescription, IFormFile? noteImage)
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var note = new Note
            {
                UserID = user.UserID,
                NoteTitle = noteTitle,
                NoteDescription = noteDescription,
                CreatedDate = DateTime.Now
            };

            if (noteImage != null && noteImage.Length > 0)
            {
                var fileName = Path.GetFileName(noteImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pictures", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await noteImage.CopyToAsync(stream);
                }

                note.NoteImage = $"/pictures/{fileName}";
            }
            else
            {
                note.NoteImage = null;
            }

            await _noteService.AddAsync(note);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> NoteDetails(int noteId)
        {
            var note = await _noteService.GetByIdAsync(noteId);

            if (note == null)
            {
                return NotFound();
            }

            var viewModel = new HomeViewModel
            {
                NoteID = noteId,
                NoteTitle = note.NoteTitle,
                NoteDescription = note.NoteDescription,
                NoteImage = note.NoteImage
            };

            return PartialView("NoteDetailsPartial", viewModel);
        }

        public async Task<IActionResult> ListNotes()
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var notes = await _noteService.GetAllByUserIdAsync(user.UserID);
            return View(notes);
        }

        public PartialViewResult StatusPartial()
        {
            return PartialView();
        }

        public PartialViewResult GoalPartial()
        {
            return PartialView();
        }

    }
}

