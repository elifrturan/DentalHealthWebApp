﻿using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace DentalHealthApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserDal _userDal;
        private readonly IUserService _userService;

        public RegisterController(IUserDal userDal, IUserService userService)
        {
            _userDal = userDal;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user, string password, string confirmPassword)
        {
            // Validasyon
            if (!RegisterRules.AreRequiredFieldsNotEmpty(user.UserFullName, user.UserEmail, password, user.UserBirthDate))
            {
                ModelState.AddModelError("", "Tüm alanları doldurmalısınız.");
                return View();
            }

            if (!RegisterRules.IsValidEmail(user.UserEmail))
            {
                ModelState.AddModelError("", "Geçerli bir e-posta adresi giriniz.");
                return View();
            }

            if (!RegisterRules.IsValidPassword(password))
            {
                ModelState.AddModelError("", "Şifre en az 8 karakter uzunluğunda olmalı, büyük harf, küçük harf ve rakam içermelidir.");
                return View();
            }

            if (!RegisterRules.IsPasswordConfirmed(password, confirmPassword))
            {
                ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                return View();
            }

            if (_userDal.GetAll().Any(u => u.UserEmail == user.UserEmail))
            {
                ModelState.AddModelError("", "Bu e-posta adresi zaten kayıtlı.");
                return View();
            }

            // Kullanıcıyı ekle
            user.AccountCreateDate = DateTime.Now;
            user.AccountUpdateDate = DateTime.Now;
            user.UserPassword = EncryptPassword(password);
            _userService.Add(user);

           

            return RedirectToAction("Index", "User");
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

