using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Signers;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using static ScrumProject.ViewModels.ViewApptViewModel;

namespace ScrumProject.Controllers
{
    public class ViewApptController : Controller
    {


        private Repository<Appointment> appt { get; set; }
        //private Repository<AppointmentType> apptType { get; set; }

        public ViewApptController(ScrumProjectContext ctx)
        {
            appt = new Repository<Appointment>(ctx);
            //apptType = new Repository<AppointmentType>(ctx);
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

        //public IActionResult Index()
        //{
        //    var today = DateTime.Today.AddDays(-4);


        //    //var upcomingAppointments = appt.List(new QueryOptions<Appointment> { OrderBy = a => a.RequestDate }).ToList();

        //    ViewApptViewModel model = new ViewApptViewModel();
        //    model.AppointmentTypes = apptType.List(new QueryOptions<AppointmentType> { OrderBy = a => a.AppointmentTypeId }).ToList();

        //    //use types to create select list items
        //    foreach (var type in model.AppointmentTypes)
        //    {
        //        model.TypeSelectList.Add(new SelectListItem { Text = type.Description, Value = type.AppointmentTypeId.ToString() });
        //    }

        //    // Set the SelectedAppointmentType property
        //    model.SelectedAppointmentType = model.AppointmentTypes.FirstOrDefault();

        //    return View(model);

        //}
    }
}
