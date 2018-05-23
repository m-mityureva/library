using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public interface IReturnRequestRepository
    {
        IQueryable<ReturnRequest> ReturnRequests { get; }
        void SaveReturnRequest(ReturnRequest returnRequest);
    }
}
