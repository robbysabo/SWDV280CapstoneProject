using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;

namespace ScrumProject.Controllers
{
    public class BookApptController : Controller
    {
        //setting up db context
        private ScrumProjectContext context { get; set; }
        public BookApptController(ScrumProjectContext ctx) 
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            BookApptViewModel model = new BookApptViewModel();
            model.AppointmentTypes = context.AppointmentTypes.ToList();

            return View(model);
        }
    }
}
