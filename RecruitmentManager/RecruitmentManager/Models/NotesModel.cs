using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RecruitmentManager.Models
{
    public class NotesModel
    {
        public Guid ID { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid OfferID { get; set; }
        [ForeignKey("OfferID")]
        public RecrutationModel Offer { get; set; }
    }
}
