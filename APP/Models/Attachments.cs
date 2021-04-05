using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
    public class Attachments
    {
        public string uploadFile { set; get; }

        public string Filename { get; set; }

        public string FileSize { get; set; }
        public string Creater { get; set; }
        public string DateTime { get; set; }
        public string Path { get; set; }

        public int Id { get; set; }

        public SPAHeader spaheader { get; set; }
    }
}
