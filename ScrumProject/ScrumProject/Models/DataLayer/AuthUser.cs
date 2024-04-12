using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumProject.Models.DataLayer
{
    public class AuthUser: IdentityUser
    {
        [NotMapped]
        public List<string> RoleNames { get; set;}
    }
}
