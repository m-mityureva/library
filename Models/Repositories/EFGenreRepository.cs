using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class EFGenreRepository : IGenreRepository
    {
        private ApplicationDbContext context;

        public EFGenreRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Genre> Genres  => context.Genres;
    }
}
