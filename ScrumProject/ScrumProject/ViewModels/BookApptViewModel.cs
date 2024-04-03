using ScrumProject.Models;
using ScrumProject.Models.DataLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ScrumProject.ViewModels
{
    public class BookApptViewModel
    {
        public BookApptViewModel()
        { 
            Appointment = new Appointment { AppointmentType = SelectedAppointmentType };
        }
        public Appointment Appointment { get; set; }
        public AppointmentType? SelectedAppointmentType { get; set; }
        public List<AppointmentType> AppointmentTypes { get; set; } = new List<AppointmentType>();
        public List<SelectListItem> TypeSelectList { get; set; } = new List<SelectListItem>();

    }
}
