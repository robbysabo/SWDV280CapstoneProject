using Microsoft.AspNetCore.Mvc;

namespace ScrumProject.Areas.Admin.Models
{
    public class DeadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
