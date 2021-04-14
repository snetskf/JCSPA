using APP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.ViewModel
{
    public class ListViewModel
    {
        public List<SPAHeader> SPAHeaders;

        public int nextpagenumber { get; set; } 

        public int startpage { get; set; }

        public int pagesize { get; set; }
        public int newpagesize { get; set; }

        public int totalpages { get; set; }

        public string searchvalue { get; set; }

        public string sortfield { get; set; }

        public string sortseq { get; set; }
    }
}
