using BusinessLayer.Abstract;
using DentalHealthApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DentalHealthApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHealthRecordService _healthRecordService;
        private readonly IRecommendationService _recommendationService;

        public HomeController(IUserService userService, IHealthRecordService healthRecordService, IRecommendationService recommendationService)
        {
            _userService = userService;
            _healthRecordService = healthRecordService;
            _recommendationService = recommendationService;
        }
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var lastWeekHealthRecords = await _healthRecordService.GetHealthRecordsForLast7DaysAsync(user.UserID);
            var recommendation = await _recommendationService.GetRandomRecommendationAsync();

            var model = new HomeViewModel
            {
                UserName = user.UserFullName,
                LastWeekHealthRecords = lastWeekHealthRecords,
                RandomRecommendation = recommendation
            };

            return View(model);
        }

        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }

        public PartialViewResult SidebarPartial()
        {
            return PartialView();
        }

        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
    }
}
