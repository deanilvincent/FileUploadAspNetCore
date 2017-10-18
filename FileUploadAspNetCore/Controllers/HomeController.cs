using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileUploadAspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FileUploadAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment envhosting;

        public HomeController(IHostingEnvironment envhosting)
        {
            this.envhosting = envhosting;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ImportFile(IFormFile file)
        {
            if (file != null)
            {
                var uploadPath = Path.Combine(envhosting.WebRootPath, "uploads");

                using (var fileStream = new FileStream(Path.Combine(uploadPath, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // check the uploads folder
            }

            return RedirectToAction("Index");
        }
    }
}
