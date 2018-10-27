using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace RecruitmentManager.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(3, ErrorMessage = "Minimum 3 znaki")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [PasswordPropertyText]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
