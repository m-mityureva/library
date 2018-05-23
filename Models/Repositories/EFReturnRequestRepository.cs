using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFReturnRequestRepository : IReturnRequestRepository
    {
        private ApplicationDbContext context;

        public EFReturnRequestRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ReturnRequest> ReturnRequests => context.ReturnRequests.Include(r=>r.UserBook);

        public void SaveReturnRequest(ReturnRequest returnRequest)
        {
            context.Attach(returnRequest.UserBook);

            if (returnRequest.Id == 0)
            {
            context.ReturnRequests.Add(returnRequest);
            }
            else
            {
                ReturnRequest dbEntry = context.ReturnRequests.FirstOrDefault(r => r.Id == returnRequest.Id);
                if (dbEntry != null)
                {
                    dbEntry.Quantity = returnRequest.Quantity;
                    dbEntry.Approved = returnRequest.Approved;
                }
            }
            context.SaveChanges();
        }
    }
}
