using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
                                           .Include(o => o.Lines)
                                           .ThenInclude(l => l.Book);

        public void SaveOrder(Order order)
        {
            foreach (var line in order.Lines)
            {
                line.Book = context.Books.FirstOrDefault(b => b.Id == line.Book.Id);
            }
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }

    }
}
