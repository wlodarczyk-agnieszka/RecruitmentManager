using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace RecruitmentManager.Models
{
    public class RecrutationModel
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name="Nazwa stanowiska")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Firma")]
        public string Company { get; set; }

        [Display(Name = "Opis stanowiska")]
        public string JobDescription { get; set; }

        [Display(Name = "Status")]
        public string Status {get;set;}

        [Display(Name = "Źródło oferty")]
        public string Source { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [Display(Name = "Data wysłania aplikacji")]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        public bool Archives { get; set; } = false;
        public bool DirectApply { get; set; } = false;

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public IdentityUser User { get; set; }

        public ICollection<NotesModel> Notes { get; set; } 

        public ICollection<StatusChangesModel> StatusChangeses { get; set; }
    }
}
