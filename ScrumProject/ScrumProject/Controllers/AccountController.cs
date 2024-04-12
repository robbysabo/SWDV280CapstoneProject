using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ScrumProject.Controllers
{
    public class AccountController : Controller
    {
        //dependency injection
        private UserManager<AuthUser> UserManager;
        private SignInManager<AuthUser> SignInManager;

        public AccountController(UserManager<AuthUser> userManager, SignInManager<AuthUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region Register
        //get
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new AuthUser { UserName = model.Username };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded) //if user is created successfully sign in/redirect to home
                {
                    bool isPersistent = false;
                    await SignInManager.SignInAsync(user, isPersistent);
                    return RedirectToAction("Index", "Home");
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
        #endregion Register

        #region Logout
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion Logout

        #region LogIn
        //get mapping
        [HttpGet]
        public IActionResult LogIn() //initializes return url and sets in vm
        {
            var model = new LoginViewModel();
            return View(model);
        }

        //post mapping
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(
                    model.Username, model.Password, isPersistent: model.RememberMe, //if isPersisent true use persistent cookie, otherwise use session cookie
                    lockoutOnFailure: false);

                if (result.Succeeded) 
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username/password."); //validation
            return View(model);
        }
        #endregion LogIn

        public ViewResult AccessDenied() { return View(); } //access denied route

    }
}
