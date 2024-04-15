using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;

namespace ScrumProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        //dependency injection
        private UserManager<AuthUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<AuthUser> userMngr,
            RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }

        //index
        public async Task<IActionResult> Index()
        {
            List<AuthUser> users = userManager.Users.ToList();

            foreach (AuthUser user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                user.RoleNames = roles.ToList(); //get each role per user

            }
            UserViewModel model = new UserViewModel
            {
                Users = users,
                Roles = roleManager.Roles
            };
            return View(model);
        }

        #region CRUD operations
        //delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AuthUser user = await userManager.FindByIdAsync(id); //find user
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user); //if found delete
                if (!result.Succeeded) // if failed
                {
                    //generate error message
                    string errorMessage = "";
                    foreach (IdentityError error in result.Errors)
                    {
                        errorMessage += error.Description + " | ";
                    }
                    TempData["message"] = errorMessage;
                }
            }
            return RedirectToAction("Index"); //redirect to index
        }

        //ADD
        //get
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Add(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new AuthUser { UserName = model.Username };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        //add/remove admin
        [HttpPost]
        public async Task<IActionResult> AddToAdmin(string id)
        {
            IdentityRole adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                TempData["message"] = "Admin role does not exist. "
                    + "Click 'Create Admin Role' button to create it.";
            }
            else
            {
                AuthUser user = await userManager.FindByIdAsync(id);
                await userManager.AddToRoleAsync(user, adminRole.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmin(string id)
        {
            AuthUser user = await userManager.FindByIdAsync(id);
            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        //delete/create role
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }
        #endregion CRUD operations

    }
}
