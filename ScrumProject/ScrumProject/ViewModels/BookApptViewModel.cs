using ScrumProject.Models;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.ViewModels
{
    public class BookApptViewModel
    {
        public BookApptViewModel()
        { 
            Appointment = new Appointment();
        }
        public Appointment Appointment { get; set; }
        public List<AppointmentType> AppointmentTypes { get; set; } = new List<AppointmentType>();

    }
}
