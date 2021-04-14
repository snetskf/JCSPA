using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
    public class SPARouting
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Email { get; set; }

        public string IsPrimary { get; set; }

        public string RequestType { get; set; }

       public int SortId { get; set; }
    }
}
