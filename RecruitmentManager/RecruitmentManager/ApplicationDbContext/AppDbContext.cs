using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruitmentManager.Models;
using RecruitmentManager.ViewModels;

namespace RecruitmentManager.ApplicationDbContext
{
    public class AppDbContext : IdentityDbContext
    {
        // dodanie modeli do kontekstu
        public DbSet<RecrutationModel> Recrutation { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
        public DbSet<StatusChangesModel> StatusChangeses { get; set; }
        public DbSet<StatusesModel> DbStatuses { get; set; }
        public DbSet<AnnotationModel> Annotations { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RecruitmentManager.ViewModels.RegisterViewModel> RegisterViewModel { get; set; }
    }
}
