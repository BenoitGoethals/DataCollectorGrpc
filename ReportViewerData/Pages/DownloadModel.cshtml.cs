using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportViewerData.Pages
{
    public class DownloadModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public DownloadModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult OnGet(string name)
        {
            var filePath = Path.Combine(_env.WebRootPath, "files", name);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", name);
        }
    }
}
