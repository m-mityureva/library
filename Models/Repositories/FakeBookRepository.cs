using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class FakeBookRepository /*: IBookRepository*/
    {
        public IQueryable<Book> Books => new List<Book> {
            new Book { Title = "ASP.NET Core MVC", Author = "Фримен А.", Genre = new Genre { Title = "Компьютерная литература" } },
            new Book { Title = "Идиот", Author = "Достоевский", Genre = new Genre { Title = "Роман" } },
            new Book { Title = "Алиса в Зазеркалье", Author = "Льюис Кэрролл", Genre = new Genre { Title = "Сказка" } },
        }.AsQueryable<Book>();
    }
}
