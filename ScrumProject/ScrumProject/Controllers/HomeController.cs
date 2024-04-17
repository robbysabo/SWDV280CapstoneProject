using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models;
using System.Diagnostics;
using ScrumProject.Models.DataLayer;
namespace ScrumProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ScrumProjectContext _context;
        public HomeController(ILogger<HomeController> logger, ScrumProjectContext ctx)
        {
            _logger = logger;
            _context = ctx;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            var model = _context.WorkerImages
                .OrderBy(m => m.ImageId)
                .ToList();
            ViewBag.ImageSrc = _context.WorkerImages.ToList();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
