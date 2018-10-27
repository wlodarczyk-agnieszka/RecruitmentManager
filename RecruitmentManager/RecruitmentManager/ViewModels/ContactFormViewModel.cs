using System.ComponentModel.DataAnnotations;

namespace RecruitmentManager.ViewModels
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Twoje imię")]
        public string Name { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [EmailAddress]
        [Display(Name = "Twóje email")]
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Temat")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Wiadomość")]
        public string Message { get; set; }
    }
}
