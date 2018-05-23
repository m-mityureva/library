using Library.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Repositories
{
    public class MenuManager : IMenuManager
    {
        private AppIdentityDbContext context;

        private List<MenuItem> items => new List<MenuItem>
        {
            new MenuItem{Name = MenuItems.UserBooks, Controller = "Book", Action = "UserList"},
            new MenuItem{Name = MenuItems.UserReturnRequests, Controller = "ReturnRequest", Action = "UserList"},
            new MenuItem{Name = MenuItems.Orders, Controller = "Order", Action = "List"},
            new MenuItem{Name = MenuItems.ReturnRequests, Controller = "ReturnRequest", Action = "List"},
            new MenuItem{Name = MenuItems.AllBooks, Controller = "Admin", Action = "Index"},
        };

        public MenuManager(AppIdentityDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<MenuItem> GetMenuItems(string userName)
        {
            IdentityUser user = context.Users.FirstOrDefault(u => u.UserName == userName);
            IEnumerable<string> userRoles = context.UserRoles
                                            .Where(r => r.UserId == user.Id)
                                            .Select(r => r.RoleId).ToList();

            IQueryable<AppMenuItem> appMenuItems = context.Roles
                                            .Where(r => userRoles.Contains(r.Id))
                                            .SelectMany(r => r.MenuItems); 

            IEnumerable<MenuItem> result = new List<MenuItem>();
            foreach (AppMenuItem item in appMenuItems)
            {
                result = result.Append(items.FirstOrDefault(i => i.Name == item.Name));
            }

            return result;
        }
    }
}
