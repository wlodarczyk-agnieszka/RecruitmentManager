using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecruitmentManager.UtilityClasses;

namespace RecruitmentManager.Models
{
    public class StatusChangesModel
    {
        public int ID { get; set; }

        public DateTime ChangeDate { get; set; } = DateTime.Now;
        public string NewStatus { get; set; }

        public Guid OfferID { get; set; }
        [ForeignKey("OfferID")]
        public RecrutationModel Offer { get; set; }
    }
}
