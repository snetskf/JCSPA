using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using APP.Data;
using Microsoft.AspNetCore.Http;
using APP.ViewModel;

namespace APP.Models
{
    public class SPAHeaderRepository:IRequestData
    {
        public AppDbContext _context;

        public SPAHeaderRepository(AppDbContext context)
        {
            _context = context;
        }

        public int pagecount { get; set; }

        public int currentpage { get; set; }

        public int maxrows { get; set; }

        public IEnumerable<SPAHeader> HeaderRequests { get {return _context.SPAHeader.ToList(); } }

        public async Task<List<SPARequestRouting>> CreateRouting(HeaderViewModel model)
        {

            List<SPARequestRouting> spa = new List<SPARequestRouting>();
            List<SPARouting> sp = new List<SPARouting>();


            sp = _context.SPARouting.ToList();

            foreach (var item in sp)
            {
                spa.Add(new SPARequestRouting() { Spaheaderid = model.header, Email = item.Email, Status = "Pending", Type = item.Type,
                TimeStamp=null});

            }

            foreach (var item in spa)
            {
                _context.SPARequestRouting.Add(item);
                await _context.SaveChangesAsync();

            }

            return spa;
        }

        public SPAHeader GetHeader(SPAHeader header)
        {
            return _context.SPAHeader.Where(x=>x.Guid==header.Guid).FirstOrDefault();
        }
        public List<SPAHeader> ListBySearch(string searchvalue)
        {
           
            int outvalue = 0;
            bool isnumeric = int.TryParse(searchvalue, out outvalue);
            if (isnumeric == true)
            {
                return _context.SPAHeader.Where(x => x.Id.ToString().Contains(searchvalue) || x.QtyRequired.ToString().Contains(searchvalue)).ToList();
            }
            else
            { 
                return _context.SPAHeader.Where
                   (x => x.Creater.Contains(searchvalue == null ? "" : searchvalue) ||
                   ( x.CFTTeam.Contains(searchvalue == null ? "" : searchvalue))).ToList();

                //return _context.SPAHeader.Where( (x => x.CFTTeam != null && x =>x.Creater != null)
                // (x => x.CFTTeam.ToString().Contains(searchvalue==null?"":searchvalue)
                //|| x.Creater.ToString().Contains(searchvalue == null ? "" : searchvalue))).ToList();

            }
        }


        public async Task<HeaderViewModel> SavefileAsync(List<IFormFile> fileuploaded,string _Guid, HeaderViewModel model)
        {

            List<Attachments> listattachment = new List<Attachments>();

            foreach (var item in fileuploaded)
            {
                if (item.Length >= 10000000) { model.FileUploadMsg= item.FileName + " ,File size can not be more than 10mb."; }
                else
                {


                    if (!System.IO.Directory.Exists(@"\\w2811\upload\SPA\" + _Guid))
                    {
                        System.IO.Directory.CreateDirectory(@"\\w2811\upload\SPA\" + _Guid);
                    }
                    using (var stream = new FileStream(@"\\w2811\upload\SPA\" + _Guid + @"\" + item.FileName, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                        listattachment.Add(new Attachments
                        {
                            Creater = model.header.Creater,
                            DateTime = model.header.DateCreated.ToString(),
                            Filename = item.FileName,
                            FileSize = item.Length.ToString(),
                            Path = @"\\w2811\upload\SPA\" + _Guid + @"\" + item.FileName,
                            spaheader = model.header
                        });
                    }
                }
            }

            foreach (var attachment in listattachment)
            {
                _context.SPAAttachments.Add(attachment);
                await _context.SaveChangesAsync();
            } 

            model.Attachments = listattachment;
            return  model;

        }
    }
}
