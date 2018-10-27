using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecruitmentManager.ViewModels;

namespace RecruitmentManager.Controllers
{
    public class HomeController : Controller
    {
        List<SelectListItem> subjects = new List<SelectListItem>
        {
            new SelectListItem {Value = "Pomysły i sugestie", Text = "Pomysły i sugestie"},
            new SelectListItem {Value = "Problem", Text = "Problem"},
            new SelectListItem {Value = "Błąd", Text = "Błąd"},
            new SelectListItem {Value = "Propozycja współpracy", Text = "Propozycja współpracy"},
            new SelectListItem {Value = "Inne", Text = "Inne"}
        };

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewBag.EmailSubject = subjects;
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.EmailSubject = subjects;
                return View(model);
            }

            var mailbody = $"Wiadomość od {model.Name} ({model.FromEmail}):\n{model.Message}";

            using (var message = new MailMessage(model.FromEmail, "wlodarczyk.agnieszka86@gmail.com"))
            {
                message.To.Add(new MailAddress("wlodarczyk.agnieszka86@gmail.com"));
                message.From = new MailAddress(model.FromEmail);
                message.Subject = $"[RM] {model.Subject}";
                message.Body = mailbody;

                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("wlodarczyk.agnieszka86@gmail.com", "");

                    smtpClient.Send(message);
                }
            }

            TempData["Info"] = "Wiadomość wysłana";
            return View("Index");
        }
    }
}
