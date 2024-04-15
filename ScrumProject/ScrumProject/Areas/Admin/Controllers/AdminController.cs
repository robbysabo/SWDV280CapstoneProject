using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Mono.TextTemplating;
using Newtonsoft.Json;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using ScrumProject.ViewModels.Admin;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ScrumProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        private UserManager<AuthUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<AuthUser> userMngr,
            RoleManager<IdentityRole> roleMngr)
        {
            userManager = userMngr;
            roleManager = roleMngr;
        }
        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            var rolelist = roleManager.Roles.ToList();
            List<AuthUser> users = userManager.Users.ToList();

            foreach (AuthUser user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                user.RoleNames = roles.ToList(); //get each role per user

            }
            AdminAdd model = new AdminAdd();
            {
                model.Users = users;
                model.Roles = rolelist;
            };
            foreach(IdentityRole Role in model.Roles)
            {
               string STRrole = Role.ToString();
                new SelectListItem { Value = STRrole, Text = STRrole };
            }
  
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> adder(AdminAdd model)
        {

            
            //TempData["Model"] = model;
            AdminAdd carryon = model;
            //model.UserData.UserType.Description = value;
            //queryresult = 
            //model.UserData.UserType = model.UserType;
            //model.UserData.UserType.Description = temp;
            
                if (ModelState.IsValid)
                {
                    var user = new AuthUser { UserName = model.UserData.UserName, Email = model.UserData.Email };
                    var result = await userManager.CreateAsync(user, model.temp_password);

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
                        string modelstate = JsonConvert.SerializeObject(result.Errors);
                        HttpContext.Session.SetString("ModelState", modelstate);

                        string serializedModel = JsonConvert.SerializeObject(model);

                        // Store the serialized model in session state
                        HttpContext.Session.SetString("Model", serializedModel);

                    }
                    return RedirectToAction("Fail", carryon);
                }
                else
                {
                    ModelState.AddModelError("", "All fields required.");
                    return Content("<script>" + "alert(\"Something has gone very wrong, we are sending you to the void to think about your sins\")" + "</script>", "text/html");
                }
            }


        
        public async Task<ActionResult> Fail(AdminAdd carryon)
        {
            
            string serializedModel = HttpContext.Session.GetString("Model");
            string ModelErrors = HttpContext.Session.GetString("ModelState");
            if (serializedModel != null)
            {
                
                // Deserialize the JSON string to reconstruct the model object
                var model = JsonConvert.DeserializeObject<AdminAdd>(serializedModel);
                var errors = JsonConvert.DeserializeObject<IEnumerable<IdentityError>>(ModelErrors);
                var rolelist = roleManager.Roles.ToList();
                List<AuthUser> users = userManager.Users.ToList();
                model.Roles = rolelist;
                model.Errors = errors;
                model.Users = users;    
                model.shouldReload = true;
                model.formtype = "newuser";
                foreach (AuthUser user in users)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    user.RoleNames = roles.ToList(); //get each role per user

                }
                return View("Index", model);
            }
            else
            {
                // Handle case where model is not found in session
                return RedirectToAction("Index");
            }
            
        }


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
            if (id != null)
            {
                IdentityRole role = await roleManager.FindByIdAsync(id);
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded) { }
                return RedirectToAction("Index");
            }
            else { return RedirectToAction("Index"); }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdminRole()
        {
            var result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (result.Succeeded) { }
            return RedirectToAction("Index");
        }


    }
}

