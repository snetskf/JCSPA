using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
    public class SPARequestRouting
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        
        public DateTime? TimeStamp { get; set; }

        public SPAHeader Spaheaderid { get; set; }
    }
}
