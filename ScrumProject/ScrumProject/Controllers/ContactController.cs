using Microsoft.AspNetCore.Mvc;
using ScrumProject.ViewModels;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using static System.Console;

namespace ScrumProject.Controllers {
    public class ContactController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel model) {
            if (ModelState.IsValid) {
                //SendEmail(model); [Disabled for now, it is sort of working as intended. However was only able to send an email successfully using 'scrumswdv280@outlook.com' within the contact form]
                return RedirectToAction("Confirmation");
            }
            
            return View(model);
        }

        public IActionResult Confirmation() {
            return View();
        }

        private static void SendEmail(ContactViewModel model) {
            try {
                // Build & Create the message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress($"{model.FirstName} {model.LastName}", model.Email)); // Who is sending the message
                message.To.Add(new MailboxAddress("", "scrumswdv280@outlook.com"));                       // Who is it being sent to
                message.Subject = model.Subject;

                // Message itself
                var bodyBuilder = new BodyBuilder {
                    TextBody = $"{model.Message}"
                };
                message.Body = bodyBuilder.ToMessageBody();

                // Connect to the SMTP server, authenticate & send the message
                using (var client = new SmtpClient()) {
                    client.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls); // Outlooks SMTP server
                    client.Authenticate("scrumswdv280@outlook.com", "scrumproj115");            
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception e) {
                // Errors that could potentially occur
                WriteLine(e);
            }
        }
    }
}