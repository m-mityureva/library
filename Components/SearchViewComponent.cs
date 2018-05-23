using Library.Models;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Components
{
    public class SearchViewComponent : ViewComponent
    {
        private IGenreRepository repository;

        public SearchViewComponent(IGenreRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.CurrentFilter = RouteData?.Values["search"];
            return View();
        }
    }
}
