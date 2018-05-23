using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFCartLineRepository : ICartLineRepository
    {
        private ApplicationDbContext context;

        public EFCartLineRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        
        public IQueryable<CartLine> CartLines => context.CartLines;
    }
}
