using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int? CurrentGenreId { get; set; }
        public string CurrentSearchString { get; set; }
    }
}
