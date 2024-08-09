using BusinessLayer.Abstract;
using DentalHealthApp.Models;
using EntityLayer.Concrete;
using Humanizer;
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
            var healthGoals = await _healthGoalService.GetAllByUserIdAsync(user.UserID);

            var model = new HomeViewModel
            {
                UserName = user.UserFullName,
                LastWeekHealthGoals = lastWeekHealthGoals,
                RandomRecommendation = recommendation,
                HealthRecords = healthRecords,
                Notes = notes,
                NewNote = new Note(),
                HealthGoals = healthGoals,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHealthRecord(HomeViewModel model, int? RecordDuration, string durationType)
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            DateTime recordDateTime = DateTime.Today.Add(DateTime.Now.TimeOfDay);

            var healthRecord = new HealthRecord
            {
                GoalID = model.GoalID,
                RecordDescription = model.RecordDescription,
                RecordDate = DateTime.Now,
                RecordDuration = $"{RecordDuration.Value} {durationType}",
                IsApplied = model.IsApplied,
                RecordTime = recordDateTime
            };

            _healthRecordService.AddAsync(healthRecord);
            return RedirectToAction("Index");
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
            var userId = int.Parse(User.Identity.Name);
            var healthGoals = _healthGoalService.GetAllGoalsByUserId(userId);

            return PartialView("StatusPartial", healthGoals);
        }

        public async Task<PartialViewResult> GoalPartial()
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return PartialView("_ErrorPartial");
            }

            var userGoals = await _healthGoalService.GetAllByUserIdAsync(user.UserID);

            return PartialView(userGoals);
        }

        [HttpPost]
        public async Task<IActionResult> AddHealthGoal(string goalTitle, string goalDescription, string goalPeriod, string goalPriority)
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var goal = new HealthGoal
            {
                UserID = user.UserID,
                GoalTitle = goalTitle,
                GoalDescription = goalDescription,
                GoalPeriod = goalPeriod,
                GoalPriority = goalPriority,
                GoalCreateDate = DateTime.Now
            };

            await _healthGoalService.AddAsync(goal);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteHealthGoal(int goalId)
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var goal = await _healthGoalService.GetByIdAsync(goalId);

            if (goal == null || goal.UserID != user.UserID)
            {
                return Unauthorized();
            }

            await _healthGoalService.DeleteAsync(goalId);

            return RedirectToAction("Index");
        }

    }
}

