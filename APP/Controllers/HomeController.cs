using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APP.Models;
using APP.ViewModel;
using APP.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestData _irequestdata;

        public HomeController(ILogger<HomeController> logger,IRequestData irequestdata)
        {
            _logger = logger;
            _irequestdata = irequestdata;
        }

        public IActionResult Index()
        {
            //SPAHeader sh = new SPAHeader(DateTime.Now);
            HeaderViewModel vm = new HeaderViewModel();

            //return View(vm);

            return View(vm);
        }

        [HttpPost]
            public async Task<IActionResult>  Index(HeaderViewModel model, List<IFormFile> fileuploaded)
        {
            HeaderViewModel vm = new HeaderViewModel();
            foreach (var item in fileuploaded)
            {
                if (item.Length >= 10000000) {vm.FileUploadMsg  =item.FileName+ " ,File size can not be more than 10mb."; }
                else
                {
                    using (var stream = new FileStream(@"\\w2811\upload\SPA\" + item.FileName, FileMode.Create))
                    {
                      await  item.CopyToAsync(stream);
                    }
                }
            }

            //SPAHeader sh = new SPAHeader(DateTime.Now);
         

            //return View(vm);

            return View(vm);
        }

        public IActionResult List()
        {
            var data = _irequestdata.HeaderRequests;
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
