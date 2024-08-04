using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DentalHealthApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public LoginController(IUserService userService, IUserSessionService userSessionService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.UserEmail == email);
            if (user == null || !VerifyPassword(password, user.UserPassword))
            {
                ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
                return View(); 
            }

            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            
            var userSession = new UserSession
            {
                UserID = user.UserID,
                SessionToken = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(30),
                IsActive = true
            };
            _userSessionService.Add(userSession);

            return RedirectToAction("Index", "Home");
        }

        private bool VerifyPassword(string enteredPassword, byte[] storedPassword)
        { 
            byte[] encryptedEnteredPassword = EncryptPassword(enteredPassword);

            return storedPassword.SequenceEqual(encryptedEnteredPassword);
        }

        private byte[] EncryptPassword(string password)
        {
            string key = "secret_key";
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
                var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                var combinedBytes = new byte[keyBytes.Length + passwordBytes.Length];

                Buffer.BlockCopy(keyBytes, 0, combinedBytes, 0, keyBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, keyBytes.Length, passwordBytes.Length);

                return sha256.ComputeHash(combinedBytes);
            }

        }
    }
}
