using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
   public interface IRequestData
    {
        IEnumerable<SPAHeader> HeaderRequests { get; }

        SPAHeader GetHeader(int id);
    }
}
