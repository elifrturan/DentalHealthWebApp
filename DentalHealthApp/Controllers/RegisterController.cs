using Microsoft.AspNetCore.Mvc;

namespace DentalHealthApp.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
