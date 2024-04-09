using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.IO;

namespace ScrumProject.Controllers
{
    public class FormsController : Controller
    {
        //setting up hosting enviroment for relative file paths
        private readonly IWebHostEnvironment _environment;
        public FormsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //index
        public IActionResult Index()
        {
            return View();
        }

        //download logic/action method
        public IActionResult Download(string fileName)
        {
            //construct relative file path with filename parameter
            var path = Path.Combine(_environment.WebRootPath, "forms", fileName);

            if (!System.IO.File.Exists(path)) //if no file found throw 404
            {
                return NotFound();
            }

            //serve file to client
            var fileStream = new FileStream(path, FileMode.Open);
            return File(fileStream, "application/octet-stream", fileName);
        }
    }

       
}
