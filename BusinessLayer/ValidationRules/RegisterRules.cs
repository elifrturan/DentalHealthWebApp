using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class RegisterRules
    {
        public static bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[a-z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool AreRequiredFieldsNotEmpty(string fullName, string email, string password, DateTime birthDate)
        {
            return !string.IsNullOrWhiteSpace(fullName) &&
                   !string.IsNullOrWhiteSpace(email) &&
                   !string.IsNullOrWhiteSpace(password) &&
                   birthDate != default;
        }

        public static bool IsPasswordConfirmed(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }
    }
}
