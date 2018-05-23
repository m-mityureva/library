using Library.Models;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IGenreRepository repository;

        public NavigationMenuViewComponent(IGenreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenreId = RouteData?.Values["genreId"];
            return View(repository.Genres.OrderBy(x => x.Id));
        }

    }
}
