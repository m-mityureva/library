using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Components
{
    public class HeaderMenuViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
