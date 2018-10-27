using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentManager.ViewModels
{
    public class RegisterViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(3, ErrorMessage = "Minimum 3 znaki")]
        public string Login { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [PasswordPropertyText]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [PasswordPropertyText]
        [Compare("Password", ErrorMessage = "Hasła muszą być identyczne")]
        [Display(Name = "Powtórz hasło")]
        public string RepeatPassword { get; set; }
    }
}
