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
        private readonly AppDbContext _dbcontext; 
        readonly string _Guid ;

        public HomeController(ILogger<HomeController> logger,IRequestData irequestdata,AppDbContext dbcontext )
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
            vm.pagename = "Index";
            //return View(vm);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> IndexEdit(HeaderViewModel model, List<IFormFile> fileuploaded, string btnAction)
        {
         
            if(btnAction!=null || btnAction != "")
            {
                SPARequestRouting spa = new SPARequestRouting();
                spa = _dbcontext.SPARequestRouting.Where(x=>x.Spaheaderid.Id==model.header.Id & x.Email=="mark.allen@skf.com" & x.Status=="Pending").FirstOrDefault();
                spa.Status = btnAction;
                spa.TimeStamp = DateTime.Now;
                _dbcontext.SPARequestRouting.Update(spa);
              await  _dbcontext.SaveChangesAsync();
            }

            return View(model);


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

            IRequestData iRequestdata;

            iRequestdata = sPAHeaderRepository;
           var routinmodel=await iRequestdata.CreateRouting(model);


            model.routing =  routinmodel; /*sPAHeaderRepository.CreateRouting( model);*/

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
        public IActionResult UpdateList(ListViewModel model, int btnchangepagesizename = 1, int nextpage = 10)
        {

            if (model.sortseq == "ASC" || model.sortseq == null) { @ViewBag.seq = "DSC"; }
            if (model.sortseq == "DSC") { @ViewBag.seq = "ASC"; }
         
            IActionResult v;
            if (model.newpagesize == 0) { model.newpagesize = 10; }
            if (model.sortfield == null) { model.sortfield = "ID"; @ViewBag.seq = "ASC"; }

            ViewData["sortseq"] = @ViewBag.seq;

            v = List(model.searchvalue, model.startpage, model.newpagesize, nextpage, model.sortfield.Replace("\r\n", "").Trim());
            ViewResult result = (ViewResult)v;
            ListViewModel vm = (ListViewModel)result.ViewData.Model;

            vm.searchvalue = "";
            ViewBag.pagesize = btnchangepagesizename;
            return View("List", vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            HeaderViewModel model = new HeaderViewModel();
            model.header = _irequestdata.HeaderRequests.Where(x => x.Id == id).FirstOrDefault();
            model.pagename = "Edit";
            model.Attachments = _dbcontext.SPAAttachments.Where(x => x.Id == id).ToList();
            model.routing = _dbcontext.SPARequestRouting.Where(x => x.Spaheaderid.Id== id).ToList();
            return View(model);
        }

        [HttpGet]
            public IActionResult List(  string searchvalue,int pageindex=1, int pagesize=10,int nextpage=10, string sortlink="ID" )
        {
           
            Console.WriteLine(searchvalue);
            var k = ViewBag.pagesize;
            ListViewModel vm = new ListViewModel(); 
            //if (hiddennewpagesize != 0) { pagesize = hiddennewpagesize; }
            int originalpageindex = pageindex;
            decimal totalpages = 0;
            decimal totalcount = _irequestdata.ListBySearch(searchvalue).Count();

            decimal runningvalue = (totalcount / pagesize);

            totalpages =Math.Ceiling(runningvalue);


            int gettruecount = _irequestdata.ListBySearch(searchvalue).ToList().Count();

            gettruecount = (pageindex-1) * pagesize - gettruecount;         

            if (pageindex > 0  ) { pageindex = pageindex * pagesize - pagesize; }

            if (gettruecount > 0) { pagesize = gettruecount-pagesize; } 
            if (gettruecount < 0  & originalpageindex == totalpages) { pagesize = -1* gettruecount ; }

            Console.WriteLine(pagesize); Console.WriteLine(pageindex);

            var data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.Id).ToList().GetRange(pageindex, pagesize);
            if (ViewData["sortseq"] != null)
            {
                if (ViewData["sortseq"].ToString() == "ASC")
                {
                    switch (sortlink)
                    {

                        case "Date Created":
                            data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.DateCreated).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Date Required":
                            data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.DateRequired).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "CFT Team":
                            data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.CFTTeam).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Qty Required":
                            data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.QtyRequired).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Creater":
                            data = _irequestdata.ListBySearch(searchvalue).OrderBy(x => x.Creater).ToList().GetRange(pageindex, pagesize);
                            break;
                    }
                }

                if (ViewData["sortseq"].ToString() == "DSC")
                {
                    switch (sortlink)
                    {
                        case "ID":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.Id).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Date Created":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.DateCreated).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Date Required":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.DateRequired).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "CFT Team":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.CFTTeam).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Qty Required":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.QtyRequired).ToList().GetRange(pageindex, pagesize);
                            break;
                        case "Creater":
                            data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.Creater).ToList().GetRange(pageindex, pagesize);
                            break;
                    }
                }
            }
            //if (ViewData["sortseq"] != null)
            //{
            //    if (ViewData["sortseq"].ToString() == "DSC")
            //    {
            //        data = _irequestdata.ListBySearch(searchvalue).OrderByDescending(x => x.Id).ToList().GetRange(pageindex, pagesize);
            //    }
            //}

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
