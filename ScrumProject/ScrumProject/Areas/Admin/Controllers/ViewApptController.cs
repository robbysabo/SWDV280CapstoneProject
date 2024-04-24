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

        //MWT: added remove appt action
        public RedirectToActionResult RemoveAppt(int id)
        {
            //get appt set appt status to complete
            Appointment selectedAppt = appt.Get(id);
            selectedAppt.ApptStat = "C";

            appt.Save();

            //set tempdata for alert
            TempData["Alert"] = $"Appointment for {selectedAppt.ContactFname}, {selectedAppt.ContactLname} on {selectedAppt.RequestDate.ToString("d")}" +
                $" has been marked as complete.";

            return RedirectToAction("Index");
        }

    }
}
