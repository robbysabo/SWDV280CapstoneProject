using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Core.Types;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;

namespace ScrumProject.ViewModels.Admin
{
    public class AdminAdd
    {
        
        
        //public UserType UserType { get; set; }
        public AuthUser UserData { get; set; }
        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string temp_password { get; set; }
        public IEnumerable<IdentityRole>? Roles { get; set; }
        public IEnumerable<IdentityError>? Errors { get; set; }
        public bool? shouldReload { get; set; }
        public string? formtype { get; set; }
        public IEnumerable<AuthUser>? Users { get; set; }
    }
    

}
