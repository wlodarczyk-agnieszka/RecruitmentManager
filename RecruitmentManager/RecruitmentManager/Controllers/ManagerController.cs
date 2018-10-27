using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RecruitmentManager.ApplicationDbContext;
using RecruitmentManager.Models;
using RecruitmentManager.ViewModels;

namespace RecruitmentManager.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private AppDbContext _context { get; }
        private UserManager<IdentityUser> _usermanager { get; }

        public List<SelectListItem> Statuses { get; } = new List<SelectListItem>();

        public ManagerController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _usermanager = userManager;

            foreach (var stat in _context.DbStatuses.ToList())
            {
                Statuses.Add(new SelectListItem { Value = stat.Value, Text = stat.Text });
            }
        }

        private void ChangeStatusLog(Guid offerId, string newStatus, string oldStatus = "")
        {
            if (oldStatus != newStatus)
            {
                StatusChangesModel status = new StatusChangesModel
                {
                    OfferID = offerId,
                    NewStatus = newStatus
                };

                _context.StatusChangeses.Add(status);
                _context.SaveChanges();
            }
        }

        public IActionResult Index() // main view
        {
            var uid = _usermanager.GetUserId(User);

            List<RecrutationModel> offers = _context.Recrutation.Include(o => o.Notes)
                .Where(x => x.UserID.ToString() == uid && x.Archives == false).ToList();

            Statuses.Insert(0, new SelectListItem { Value = "Wszystko", Text = "Status (wszystkie)" });
            ViewBag.Stat = Statuses;

            return View(offers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Usr = _usermanager.GetUserId(User);
            ViewBag.Stat = Statuses;

            return View();
        }

        [HttpPost]
        public IActionResult Add(RecrutationModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _context.Recrutation.Add(model);

                _context.SaveChanges();

                ChangeStatusLog(result.Entity.ID, model.Status);

                return RedirectToAction("Index");
            }

            ViewBag.Stat = Statuses;
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewOffer(Guid id)
        {
            RecrutationModel offer = _context.Recrutation.Include(o => o.Notes).Include(s => s.StatusChangeses)
                .FirstOrDefault(x => x.ID == id);
            return View(offer);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            RecrutationModel offer = _context.Recrutation.Include(o => o.Notes).FirstOrDefault(x => x.ID == id);

            ViewBag.Usr = offer.UserID;
            ViewBag.Stat = Statuses;

            return View(offer);
        }

        [HttpPost]
        public IActionResult Edit(RecrutationModel model)
        {
            if (ModelState.IsValid)
            {
                var oldStatus = _context.Recrutation.AsNoTracking().FirstOrDefault(x => x.ID == model.ID);
                if (oldStatus != null)
                {
                    ChangeStatusLog(model.ID, model.Status, oldStatus.Status);
                }

                _context.Recrutation.Update(model);
                _context.SaveChanges();
            }
            else
            {
                ViewBag.Stat = Statuses;
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Search(string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = text.ToLower();
                var uid = _usermanager.GetUserId(User);

                List<RecrutationModel> userOffers = _context.Recrutation.Where(x =>
                    x.UserID == uid && (x.JobTitle.ToLower().Contains(text) ||
                                        x.JobDescription.ToLower().Contains(text) ||
                                        x.Company.ToLower().Contains(text))).ToList();
                List<RecrutationModel> Offers = new List<RecrutationModel>();

                if (userOffers.Any())
                {
                    foreach (var item in userOffers)
                    {
                        RecrutationModel offer = _context.Recrutation.Include(o => o.Notes)
                            .FirstOrDefault(x => x.ID == item.ID);
                        Offers.Add(offer);
                    }

                    Statuses.Insert(0, new SelectListItem { Value = "Wszystko", Text = "Pokaż wszystko" });
                    ViewBag.Stat = Statuses;
                    return View("Index", Offers);
                }

                TempData["Info"] = "Brak wyników wyszukiwania";
                return RedirectToAction("Index");
            }

            TempData["Info"] = "Wpisz coś jak chcesz przeszukać ogłoszenia :)";
            return RedirectToAction("Index");
        }


        public IActionResult DeleteOffer(Guid id)
        {
            RecrutationModel model = _context.Recrutation.FirstOrDefault(x => x.ID == id);

            if (model != null)
            {
                _context.Recrutation.Remove(model);
                _context.SaveChanges();
                TempData["Info"] = "Oferta usunięta.";
                return RedirectToAction("Archives", "Manager");
            }

            TempData["Info"] = "Nie ma takiej oferty.";
            return RedirectToAction("Archives", "Manager");
        }



        [HttpPost]
        public IActionResult AddNote(Guid id, string text)
        {
            if (!String.IsNullOrEmpty(text))
            {
                NotesModel note = new NotesModel { CreatedDate = DateTime.Now, OfferID = id, Text = text };
                _context.Notes.Add(note);
                _context.SaveChanges();

                TempData["Ok"] = "Notatka dodana.";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult RemoveNote(Guid id)
        {
            NotesModel note = _context.Notes.FirstOrDefault(x => x.ID == id);

            if (note != null)
            {
                _context.Notes.Remove(note);
                _context.SaveChanges();

                TempData["Info"] = "Notatka usunięta.";
            }

            return RedirectToAction("Index");
        }


        public IActionResult AddToArchive(Guid id)
        {
            RecrutationModel model = _context.Recrutation.FirstOrDefault(x => x.ID == id);

            if (model != null)
            {
                model.Archives = true;
                _context.Recrutation.Update(model);
                _context.SaveChanges();

                TempData["Info"] = "Oferta zarchiwizowana.";

                return RedirectToAction("Index");
            }

            TempData["Error"] = "Nie ma takiej oferty";

            return RedirectToAction("Index");
        }


        public IActionResult FromArchiveToManager(Guid id)
        {
            RecrutationModel model = _context.Recrutation.FirstOrDefault(x => x.ID == id);

            if (model != null)
            {
                model.Archives = false;
                _context.Recrutation.Update(model);
                _context.SaveChanges();

                TempData["Info"] = "Oferta przywrócona.";

                return RedirectToAction("Archives");
            }

            TempData["Info"] = "Nie ma takiej oferty";

            return RedirectToAction("Archives");
        }


        public IActionResult Archives()
        {
            var id = _usermanager.GetUserId(User);

            List<RecrutationModel> userOffers = _context.Recrutation.Include(o => o.Notes)
                .Where(x => x.UserID.ToString() == id && x.Archives == true).ToList();

            Statuses.Insert(0, new SelectListItem { Value = "Wszystko", Text = "Pokaż wszystko" });
            ViewBag.Stat = Statuses;
            return View(userOffers);
        }


        [HttpGet]
        public IActionResult Annotations()
        {
            var uid = _usermanager.GetUserId(User);
            List<AnnotationModel> Annotations = _context.Annotations.Where(x => x.UserID == uid).ToList();

            return View(Annotations);
        }

        [HttpGet]
        public IActionResult AddAnnotation()
        {
            ViewBag.Usr = _usermanager.GetUserId(User);
            return View();
        }

        [HttpPost]
        public IActionResult AddAnnotation(AnnotationModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Annotations.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Annotations");
            }

            ViewBag.Usr = _usermanager.GetUserId(User);
            return View(model);
        }

        [HttpGet]
        public IActionResult EditAnnotation(Guid id)
        {
            AnnotationModel ann = _context.Annotations.FirstOrDefault(x => x.ID == id);

            if (ann != null)
            {
                return View(ann);
            }

            TempData["Error"] = "Nie znaleziono obiektu o podanym ID.";
            return RedirectToAction("Annotations");
        }

        [HttpPost]
        public IActionResult EditAnnotation(AnnotationModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Annotations.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Annotations");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveAnnotation(Guid id)
        {
            var ann = _context.Annotations.FirstOrDefault(x => x.ID == id);

            if (ann != null)
            {
                _context.Annotations.Remove(ann);
                _context.SaveChanges();
                TempData["Ok"] = "Adnotacja usunięta.";
                return RedirectToAction("Annotations");
            }

            TempData["Error"] = "Nie ma takiej adnotacji.";
            return RedirectToAction("Annotations");
        }



        [HttpPost]
        public IActionResult ChangeStatus(Guid id, string selectedStatus)
        {
            RecrutationModel offer = _context.Recrutation.FirstOrDefault(x => x.ID == id);

            if (offer != null && offer.Status != selectedStatus)
            {
                ChangeStatusLog(offer.ID, selectedStatus, offer.Status);

                offer.Status = selectedStatus;
                _context.Recrutation.Update(offer);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Statistics()
        {
            var uid = _usermanager.GetUserId(User);

            Dictionary<string, int> ActualStats = new Dictionary<string, int>();
            Dictionary<string, int> ArchivesStats = new Dictionary<string, int>();
            Dictionary<string, int> AllStats = new Dictionary<string, int>();

            //----------------------Actual

            ActualStats.Add("Wszystkie", _context.Recrutation.Count(x => x.UserID == uid && x.Archives == false));

            foreach (var item in Statuses)
            {
                int i = _context.Recrutation.Count(x =>
                    x.Status == item.Text && x.UserID == uid && x.Archives == false);

                ActualStats.Add(item.Text, i);
            }

            //----------------------Archives

            ArchivesStats.Add("Wszystkie", _context.Recrutation.Count(x => x.UserID == uid && x.Archives));

            foreach (var item in Statuses)
            {
                int i = _context.Recrutation.Count(x => x.Status == item.Text && x.UserID == uid && x.Archives);

                ArchivesStats.Add(item.Text, i);
            }


            //----------------------All

            AllStats.Add("Wszystkie", _context.Recrutation.Count(x => x.UserID == uid));

            foreach (var item in Statuses)
            {
                int i = _context.Recrutation.Count(x => x.Status == item.Text && x.UserID == uid);

                AllStats.Add(item.Text, i);
            }


            //----------------------

            StatisticsViewModel s = new StatisticsViewModel
            {
                Actual = ActualStats,
                Archives = ArchivesStats,
                All = AllStats
            };

            return View(s);

        }

        [HttpGet]
        public IActionResult Options()
        {
            List<string> stats = _context.DbStatuses.Select(s => s.Text).ToList();
            ViewBag.Stats = stats;
            return View();
        }



        [HttpPost]
        public IActionResult Options(FileUpload imported)
        {
            List<string> stats = _context.DbStatuses.Select(s => s.Text).ToList();

            if (imported.File == null || Path.GetExtension(imported.File.FileName) != ".xlsx" )
            {
                ViewBag.Stats = stats;
                TempData["Error"] = "Błędny format pliku. System obsługuje tylko .xlsx (Excel 2007+).";
                return View();
            }

            try
            {
                using (ExcelPackage package = new ExcelPackage(imported.File.OpenReadStream())) 
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.Rows;

                    var uid = _usermanager.GetUserId(User);
                    int count = 0;

                    for (int row = 2; row <= rowCount; row++) // row = 1 -> header
                    {
                        // data validation
                        DateTime d;
                        if (!DateTime.TryParse(worksheet.Cells[row, 4].Value.ToString(), out d))
                        {
                            d = DateTime.Today;
                        }
                     
                        // insert into DB
                        var model = new RecrutationModel();
                        model.JobTitle = worksheet.Cells[row, 1].Value.ToString();
                        model.Company = worksheet.Cells[row, 2].Value.ToString();
                        model.Status = stats.Contains(worksheet.Cells[row, 3].Value.ToString().Trim()) ? worksheet.Cells[row, 3].Value.ToString().Trim() : stats[0];
                        model.ApplicationDate = d;
                        model.Source = worksheet.Cells[row, 5].Value.ToString();
                        model.JobDescription = worksheet.Cells[row, 6].Value.ToString();
                        model.UserID = uid;

                        var result = _context.Recrutation.Add(model);
                        _context.SaveChanges();
                        count++;
                        ChangeStatusLog(result.Entity.ID, result.Entity.Status);

                        if (worksheet.Cells[row, 7].Value != null)
                        {
                            var n = new NotesModel();
                            n.Text = worksheet.Cells[row, 7].Value.ToString();
                            n.OfferID = result.Entity.ID;

                            _context.Notes.Add(n);
                            _context.SaveChanges();
                        }
                    }

                    TempData["Info"] = $"Załadowano rekordów: {count}.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Stats = stats;
                TempData["Error"] = $"Błąd: {ex.Message}";
                return View();
            }
            
           ViewBag.Stats = stats;
           return View();
        }
    }
}