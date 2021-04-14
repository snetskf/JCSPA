using APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.ViewModel
{
    public class HeaderViewModel
    {
        public HeaderViewModel()
        {
            header = new SPAHeader(DateTime.Now.AddDays(-3)); 

            //  header.Creater = "Bhv";
            //  header.Creater = "Bhv";
            //Attachments =
            //new List<Attachments> { new Attachments { Filename = "test.txt", FileSize = "10mb", Creater = "B" },
            // new Attachments { Filename = "test2.txt", FileSize = "120mb", Creater = "2B" }
            //};
        }

        public SPAHeader header { get; set; }
        public Attachments attachment { get; set; }
        public IEnumerable<Attachments> Attachments { get; set; }

        public string FileUploadMsg { get; set; }
        
        public string CurrentGuid { get; set; }

        public IEnumerable<SPARequestRouting> routing { get; set; }

        public string pagename { get; set; }
    }

}
