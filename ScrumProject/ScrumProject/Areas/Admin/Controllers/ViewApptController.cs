using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ViewApptController : Controller
    {

        private Repository<Appointment> appt { get; set; }

        public ViewApptController(ScrumProjectContext ctx)
        {
            appt = new Repository<Appointment>(ctx);
        }


        public IActionResult Index()
        {
            var upcomingAppointments = appt.List(new QueryOptions<Appointment> { OrderBy = a => a.RequestDate }).ToList();

            return View(upcomingAppointments);
        }

        public IActionResult Todays()
        {
            var upcomingAppointments = appt.List(new QueryOptions<Appointment> { OrderBy = a => a.RequestDate }).ToList();


            return View(upcomingAppointments);
        }

    }
}
