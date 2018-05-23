using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFBookRepository : IBookRepository
    {
        private ApplicationDbContext context;

        public EFBookRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Book> Books => context.Books.Include(b => b.Genre);

        public Book DeleteBook(int bookId)
        {
            Book book = context.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                context.Remove(book);
                context.SaveChanges();
            }

            return book;
        }

        public void SaveBook(Book book)
        {
            if (book.Id == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.FirstOrDefault(b => b.Id == book.Id);
                if (dbEntry != null)
                {
                    dbEntry.Title = book.Title;
                    dbEntry.Author = book.Author;
                    dbEntry.Available += book.Count - dbEntry.Count;
                    dbEntry.Count = book.Count;
                    if (book.Genre != null)
                        dbEntry.Genre = context.Genres.FirstOrDefault(g => g.Id == book.Genre.Id);
                }
            }
            context.SaveChanges();
        }
    }
}
