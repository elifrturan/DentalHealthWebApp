using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DentalHealthApp.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DentalHealthApp.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name;
            var user = await _userService.GetByEmailAsync(userEmail);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var model = new HomeViewModel
            {
                User = user,
                UserName = user.UserFullName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel model, string newPassword, string confirmPassword)
        {
            var user = await _userService.GetByEmailAsync(User.Identity.Name);

            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                if (!RegisterRules.IsValidPassword(model.NewPassword))
                {
                    ModelState.AddModelError("NewPassword", "Şifre en az 8 karakter uzunluğunda olmalı, büyük harf, küçük harf ve rakam içermelidir.");
                    return View(model);
                }

                if (!RegisterRules.IsPasswordConfirmed(model.NewPassword, model.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassword", "Şifreler uyuşmuyor.");
                    return View(model);
                }

                 user.UserPassword = EncryptPassword(model.NewPassword);
                
            }

            if (user.UserEmail != model.User.UserEmail)
            {
                if (await _userService.GetByEmailAsync(model.User.UserEmail) != null)
                {
                    ModelState.AddModelError("User.UserEmail", "Bu e-posta adresi zaten kullanımda.");
                    return View(model);
                }
                user.UserEmail = model.User.UserEmail;
            }

            user.UserFullName = model.User.UserFullName;
            user.UserBirthDate = model.User.UserBirthDate;
            user.AccountUpdateDate = DateTime.Now;

            try
            {
                await _userService.UpdateAsync(user);
                TempData["SuccessMessage"] = "Profil başarıyla güncellendi.";
                return RedirectToAction("Index", "Profile");
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                return View(model);
            }

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
