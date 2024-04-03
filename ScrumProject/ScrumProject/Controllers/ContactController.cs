using Microsoft.AspNetCore.Mvc;

namespace ScrumProject.Controllers {
    public class ContactController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Send() {
            return RedirectToAction("Confirmation");
        }

        public IActionResult Confirmation() {
            return View();
        }
    }
}