using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DentalHealthApp.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DentalHealthApp.Controllers
{
    public class PasswordResetController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordResetService _passwordResetService;

        public PasswordResetController(IUserService userService, IPasswordResetService passwordResetService)
        {
            _userService = userService;
            _passwordResetService = passwordResetService;
        }

        // GET: /PasswordReset
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.UserEmail == email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View();
            }

            var resetToken = Guid.NewGuid().ToString();
            var passwordReset = new PasswordReset
            {
                UserID = user.UserID,
                ResetToken = resetToken,
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddHours(1),
                IsUsed = false
            };
            _passwordResetService.Add(passwordReset);

            return View("ResetPassword", new PasswordResetViewModel { Email = email });
        }

        [HttpPost]
        public IActionResult ResetPassword(PasswordResetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetAll().FirstOrDefault(u => u.UserEmail == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(model);
            }

            var passwordReset = _passwordResetService.GetAll().FirstOrDefault(p => p.UserID == user.UserID && !p.IsUsed);
            if (passwordReset == null || passwordReset.ExpiresAt < DateTime.Now)
            {
                ModelState.AddModelError("", "Geçersiz veya süresi dolmuş token.");
                return View(model);
            }

            if (!RegisterRules.IsValidPassword(model.NewPassword))
            {
                ModelState.AddModelError("", "Parola kriterlere uymuyor. Parolanız en az 8 karakter olmalı. Büyük, küçük harf ve rakam içermelidir.");
                return View(model);
            }

            user.UserPassword = EncryptPassword(model.NewPassword);
            _userService.Update(user);

            passwordReset.IsUsed = true;
            _passwordResetService.Update(passwordReset);

            return RedirectToAction("Index", "Login");
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
