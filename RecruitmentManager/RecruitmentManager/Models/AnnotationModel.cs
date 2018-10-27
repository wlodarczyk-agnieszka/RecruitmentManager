using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManager.Models
{
    public class AnnotationModel
    {
        public Guid ID { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(3,  ErrorMessage = "Minimum 3 znaki")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(3, ErrorMessage = "Minimum 3 znaki")]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }
    }
}
