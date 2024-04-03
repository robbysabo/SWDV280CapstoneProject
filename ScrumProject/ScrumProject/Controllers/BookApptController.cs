using Microsoft.AspNetCore.Mvc;
using ScrumProject.Models;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //get request for index page
        [HttpGet]
        public IActionResult Index()
        {
            //instantiate vm, populate types
            BookApptViewModel model = new BookApptViewModel();
            model.AppointmentTypes = context.AppointmentTypes.ToList();

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
                model.Appointment.ApptStat = " "; //set open appointment status

                TempData["AlertMessage"] = "Appointment request received. Someone from our team will contact you shortly."; //success message

                context.Add(model.Appointment); //add to db
                context.SaveChanges();

                return RedirectToAction("Success");
            }
            else
            {
                //add validation error to model state
                ModelState.AddModelError("", "All fields required.");

                //instantiate vm, populate types
                model.AppointmentTypes = context.AppointmentTypes.ToList();

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
