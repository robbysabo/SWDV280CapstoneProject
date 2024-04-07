using System.ComponentModel.DataAnnotations;

namespace ScrumProject.ViewModels {
    public class ContactViewModel {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+@[a-z]+\.[a-z]{2,}$", ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Please select a subject")]
        public required string Subject { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public required string Message { get; set; }
    }
}

