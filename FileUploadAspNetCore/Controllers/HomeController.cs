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
using FileUploadAspNetCore.Variables;

namespace FileUploadAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment hostingEnv)
        {
            this.hostingEnv = hostingEnv;
        }

        public IActionResult Index()
        {
            var fileDetails = new List<FileDetails>();

            var directoryFiles = Directory.GetFiles("wwwroot/uploadsfolder");

            foreach (var item in directoryFiles)
            {
                string exactFileName = item.Replace("wwwroot/uploadsfolder\\", "");

                fileDetails.Add(new FileDetails
                {
                    Filename = exactFileName,
                    FullPath = "/Uploads/" + exactFileName
                });
            }

            return View(fileDetails);
        }

        public async Task<IActionResult> ImportFile(IFormFile file)
        {
            var uploadPath = Path.Combine(hostingEnv.WebRootPath, "uploadsfolder");

            using (var fileStream = new FileStream(Path.Combine(uploadPath, file.FileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return RedirectToAction("Index");
        }
    }
}
