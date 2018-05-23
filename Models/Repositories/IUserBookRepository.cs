using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public interface IUserBookRepository
    {
        IQueryable<UserBook> UserBooks { get; }
        void SaveUserBook(UserBook userBook);
    }
}
