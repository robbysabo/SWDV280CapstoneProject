using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.Controllers
{
    public class ViewApptController : Controller
    {
        private Repository<Appointment> appt { get; set; }

        public ViewApptController(ScrumProjectContext ctx)
        {
            appt = new Repository<Appointment>(ctx);
        }


        [HttpGet]
        public IActionResult Index()
        {
            var upcomingAppointments = appt.List(new QueryOptions<Appointment> { OrderBy = a => a.RequestDate }).ToList();

            return View(upcomingAppointments);

        }
    }
}