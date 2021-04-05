using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.Data;

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

        public SPAHeader GetHeader(int id)
        {
            throw new NotImplementedException();
        }
    }
}
