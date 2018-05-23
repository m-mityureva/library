using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class BookEditViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public Book Book { get; set; }
    }
}
