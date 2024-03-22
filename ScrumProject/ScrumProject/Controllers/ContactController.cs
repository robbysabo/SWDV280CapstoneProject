using Microsoft.AspNetCore.Mvc;

namespace ScrumProject.Controllers;

public class ContactController : Controller {
    public IActionResult Index() {
        return View();
    }
}