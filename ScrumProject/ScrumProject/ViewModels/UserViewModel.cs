using Microsoft.AspNetCore.Identity;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.ViewModels
{
    public class UserViewModel
    {
        //collection of users/roles
        public IEnumerable<AuthUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
