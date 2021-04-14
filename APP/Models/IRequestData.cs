using APP.Data;
using APP.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.Models
{
   public interface IRequestData
    {
     
        IEnumerable<SPAHeader> HeaderRequests { get; }

        List<SPAHeader> ListBySearch(string searchvalue);
        SPAHeader GetHeader(SPAHeader header);

      Task<HeaderViewModel> SavefileAsync(List<IFormFile> fileuploaded, string _Guid, HeaderViewModel model);

        Task<List<SPARequestRouting>> CreateRouting(HeaderViewModel model);

    }
}
