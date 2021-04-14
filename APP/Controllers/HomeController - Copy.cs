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
    public class HomeController1 : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestData _irequestdata;
        private readonly AppDbContext _dbcontext; 
        readonly string _Guid ;

        public HomeController1(ILogger<HomeController> logger,IRequestData irequestdata,AppDbContext dbcontext )
        {
            _Guid = System.Guid.NewGuid().ToString();
            _logger = logger;
            _irequestdata = irequestdata;
            _dbcontext = dbcontext; 
        }

        public IActionResult Index()
        {
            //SPAHeader sh = new SPAHeader(DateTime.Now);
            HeaderViewModel vm = new HeaderViewModel();
            vm.CurrentGuid = _Guid;

            //return View(vm);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HeaderViewModel model, List<IFormFile> fileuploaded)
        {
            foreach (var item in fileuploaded)
            {
                if (item.Length >= 10000000) { model.FileUploadMsg = item.FileName + " ,File size can not be more than 10mb."; }
            }


            if (model.FileUploadMsg !=null) return View(model); 

            _dbcontext.SPAHeader.Add(model.header);
            await _dbcontext.SaveChangesAsync();

            //  var headerid = _irequestdata.GetHeader(model.header);
            SPAHeaderRepository sPAHeaderRepository = new SPAHeaderRepository(_dbcontext);

          model= await sPAHeaderRepository.SavefileAsync(fileuploaded, _Guid, model);
            
            return View(model);


        }

        //    public async Task<IActionResult>  Index(HeaderViewModel model, List<IFormFile> fileuploaded)
        //{
        //    HeaderViewModel vm = new HeaderViewModel();
        //    model.header.Guid = _Guid;

        //    _dbcontext.SPAHeader.Add(model.header);
        //    await _dbcontext.SaveChangesAsync();

        //  //  var headerid = _irequestdata.GetHeader(model.header);


        //    List<Attachments> listattachment = new List<Attachments>();

        //    foreach (var item in fileuploaded)
        //    {
        //        if (item.Length >= 10000000) {vm.FileUploadMsg  =item.FileName+ " ,File size can not be more than 10mb."; }
        //        else
        //        {


        //            if(!System.IO.Directory.Exists(@"\\w2811\upload\SPA\"+ _Guid))
        //            {
        //                System.IO.Directory.CreateDirectory(@"\\w2811\upload\SPA\" + _Guid);
        //            }
        //            using (var stream = new FileStream(@"\\w2811\upload\SPA\" + _Guid+@"\" + item.FileName, FileMode.Create))
        //            {
        //              await  item.CopyToAsync(stream);
        //                listattachment.Add(new Attachments
        //                {
        //                    Creater = model.header.Creater,
        //                    DateTime = model.header.DateCreated.ToString(),
        //                    Filename = item.FileName,
        //                    FileSize = item.Length.ToString(),
        //                    Path = @"\\w2811\upload\SPA\" + _Guid + @"\" + item.FileName,
        //                    spaheader = model.header
        //                }) ;
        //            }
        //        }
        //    }

        //    vm.Attachments = listattachment;           

        //    foreach (var attachment in listattachment)
        //    {
        //        _dbcontext.SPAAttachments.Add(attachment);
        //        await _dbcontext.SaveChangesAsync();
        //    }

        //    //SPAHeader sh = new SPAHeader(DateTime.Now);


        //    //return View(vm);

        //    return View(vm);
        //}


        [HttpPost]
        public IActionResult UpdateList(ListViewModel model, int btnchangepagesizename=1,   int nextpage = 10)
        { 

            IActionResult v;
            if (model.newpagesize == 0) { model.newpagesize = 10; }
            v = List(model.searchvalue, model.startpage, model.newpagesize, nextpage);
            ViewResult result = (ViewResult)v;
            ListViewModel vm =(ListViewModel) result.ViewData.Model;
            vm.searchvalue = "";
            ViewBag.pagesize = btnchangepagesizename;
            return View("List",vm);

        }

        [HttpGet]
            public IActionResult List(  string searchvalue,int pageindex=1, int pagesize=10,int nextpage=10 )
        {
            int outvalue = 0;
            bool isnumeric = int.TryParse(searchvalue,out outvalue);
            Console.WriteLine(searchvalue);
            var k = ViewBag.pagesize;
            ListViewModel vm = new ListViewModel(); 
            //if (hiddennewpagesize != 0) { pagesize = hiddennewpagesize; }
            int originalpageindex = pageindex;
            decimal totalpages = 0;
            decimal totalcount = _irequestdata.HeaderRequests.Where(x=>x.Id==Convert.ToInt32( searchvalue)).Count();

            decimal runningvalue = (totalcount / pagesize);

            totalpages =Math.Ceiling(runningvalue);


            int gettruecount = _irequestdata.HeaderRequests.ToList().Count();

            gettruecount = (pageindex-1) * pagesize - gettruecount;         

            if (pageindex > 0  ) { pageindex = pageindex * pagesize - pagesize; }

            if (gettruecount > 0) { pagesize = gettruecount-pagesize; } 
            if (gettruecount < 0 & pageindex>1 & originalpageindex == totalpages) { pagesize = -1* gettruecount ; }

            Console.WriteLine(pagesize); Console.WriteLine(pageindex);

            var data = _irequestdata.HeaderRequests.ToList().GetRange(pageindex , pagesize);

            ViewBag.pagelist = totalpages;
            ViewBag.pagenumber = originalpageindex; 

            vm.SPAHeaders = data;
            vm.nextpagenumber = nextpage;
            vm.startpage = originalpageindex;
            vm.pagesize = pagesize;
            vm.totalpages = Convert.ToInt32( totalpages);
             
              //data = _irequestdata.HeaderRequests.ToList().GetRange(2, 2);
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
