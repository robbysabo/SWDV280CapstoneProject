using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScrumProject.Models.DataAccess;

namespace ScrumProject.Controllers
{
    public class BookApptController : Controller
    {
        //dependency injection
        private Repository<Appointment> appt { get; set; }
        private Repository<AppointmentType> apptType { get; set; }

        public BookApptController(ScrumProjectContext ctx) 
        {
            appt = new Repository<Appointment>(ctx);
            apptType = new Repository<AppointmentType>(ctx);
        }

        //get request for index page
        [HttpGet]
        public IActionResult Index()
        {
            //instantiate vm, populate types
            BookApptViewModel model = new BookApptViewModel();
            model.AppointmentTypes = apptType.List(new QueryOptions<AppointmentType> {OrderBy = a => a.AppointmentTypeId }).ToList();

            //use types to create select list items
            foreach (var type in model.AppointmentTypes)
            {
                model.TypeSelectList.Add(new SelectListItem { Text = type.Description, Value = type.AppointmentTypeId.ToString() });
            }

            // Set the SelectedAppointmentType property
            model.SelectedAppointmentType = model.AppointmentTypes.FirstOrDefault();

            return View(model);
        }

        //post request for index page
        [HttpPost]
        public IActionResult Index(BookApptViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Appointment.AppointmentType = model.SelectedAppointmentType; //set appointment type
               // model.Appointment.ApptStat = " "; //set open appointment status

                appt.Insert(model.Appointment);
                appt.Save();

                return RedirectToAction("Success");
            }
            else
            {
                //add validation error to model state
                ModelState.AddModelError("", "All fields required.");

                //instantiate vm, populate types
                model.AppointmentTypes = apptType.List(new QueryOptions<AppointmentType> { OrderBy = a => a.AppointmentTypeId }).ToList();

                //use types to create select list items
                foreach (var type in model.AppointmentTypes)
                {
                    model.TypeSelectList.Add(new SelectListItem { Text = type.Description, Value = type.AppointmentTypeId.ToString() });
                }

                // Set the SelectedAppointmentType property
                model.SelectedAppointmentType = model.AppointmentTypes.FirstOrDefault();

                return View(model);
            }

        }

        //Success page
        public IActionResult Success()
        {
            return View();
        }
    }
}
