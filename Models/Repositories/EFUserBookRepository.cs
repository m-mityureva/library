using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFUserBookRepository : IUserBookRepository
    {
        private ApplicationDbContext context;

        public EFUserBookRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<UserBook> UserBooks => context.UserBooks.Include(u => u.Book);

        public void SaveUserBook(UserBook userBook)
        {
            context.Attach(userBook.Book);

            if (userBook.Id == 0)
            {
                context.UserBooks.Add(userBook);
            }
            context.SaveChanges();
        }
    }
}
