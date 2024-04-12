using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using ScrumProject.Models.DataAccess;
using ScrumProject.Models.DataLayer;
using ScrumProject.ViewModels;
using ScrumProject.ViewModels.Admin;

namespace ScrumProject.Controllers
{
    public class AdminController : Controller
    {
        private Repository<User> dbt_user { get; set; }
        private Repository<UserType> dbt_usertype { get; set; }

        public AdminController(ScrumProjectContext ctx)
        {
            dbt_user = new Repository<User>(ctx);
            dbt_usertype = new Repository<UserType>(ctx);
        }
        // GET: HomeController1
        public ActionResult Index()
        {
            
            return View();
        }

 
        [HttpPost]
        public ActionResult adder(AdminAdd model) {

            string value = dbt_usertype.Get(1).Description;
            //model.UserData.UserType.Description = value;
            //queryresult = 
            //model.UserData.UserType = model.UserType;
            //model.UserData.UserType.Description = temp;
            if (ModelState.IsValid)
            {
                
                
                dbt_user.Insert(model.UserData);
                Console.WriteLine("valid");
                dbt_user.Save();
                return View();
            }
            else
            {
                ModelState.AddModelError("", "All fields required.");
                return Content("<script>" + "alert(\"Something has gone very wrong, we are sending you to the void to think about your sins\")" + "</script>", "text/html");
            }

            
        }
    }
}
