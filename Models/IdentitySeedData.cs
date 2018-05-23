using Library.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class IdentitySeedData
    {
        private class AppUser
        {
            public string Name { get; set; }
            public string Password { get; set; }
            public List<string> Roles { get; set; }

            public AppUser(string name, string password, List<string> roles)
            {
                Name = name;
                Password = password;
                Roles = roles;
            }
        }

        private static Dictionary<string, List<string>> roles = new Dictionary<string, List<string>>
        {
            { Roles.Admin, new List<string>{ MenuItems.AllBooks, MenuItems.Orders, MenuItems.ReturnRequests} },
            { Roles.Storekeeper, new List<string>{ MenuItems.AllBooks} },
            { Roles.Reader, new List<string>{ MenuItems.UserBooks, MenuItems.UserReturnRequests } },
            { Roles.Librarian, new List<string>{ MenuItems.Orders, MenuItems.ReturnRequests} }
        };

        private static List<AppMenuItem> menuItems = new List<AppMenuItem>
        {
            new AppMenuItem{Name= MenuItems.AllBooks },
            new AppMenuItem{Name= MenuItems.Orders },
            new AppMenuItem{Name= MenuItems.ReturnRequests },
            new AppMenuItem{Name= MenuItems.UserBooks },
            new AppMenuItem{Name= MenuItems.UserReturnRequests }
        };

        private static List<AppUser> users = new List<AppUser>
        {
            new AppUser("Admin", "1qaz!QAZ", new List<string>{ "Admin" }),
            new AppUser("Storekeeper", "1qaz!QAZ", new List<string>{ "Storekeeper" }),
            new AppUser("Librarian", "1qaz!QAZ", new List<string>{ "Librarian" }),
            new AppUser("Reader", "1qaz!QAZ", new List<string>{ "Reader" })
        };

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<AppRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<AppRole>>();

            foreach (var r in roles)
            {
                AppRole role = roleManager.Roles.Include(x=>x.MenuItems).FirstOrDefault(x=>x.Name== r.Key);
                //var roleMenuItems = roleManager.Roles.FirstOrDefault(x => x.Name == r.Key).MenuItems;
                if (role == null)
                {
                    role = new AppRole { Name = r.Key };
                    await roleManager.CreateAsync(role);
                }

                List<AppMenuItem> menuItemsList = new List<AppMenuItem>();
                foreach (var item in r.Value)
                {
                    if (role.MenuItems.FirstOrDefault(i => i.Name == item) == null)
                        role.MenuItems.Add(new AppMenuItem { Name = item });
                }
                IdentityResult res = await roleManager.UpdateAsync(role);
            }

            foreach (AppUser u in users)
            {
                IdentityUser user = await userManager.FindByNameAsync(u.Name);

                if (user == null)
                {
                    user = new IdentityUser(u.Name);
                    await userManager.CreateAsync(user, u.Password);

                    foreach (string r in u.Roles)
                        await userManager.AddToRoleAsync(user, r);
                }
            }

        }
    }
}
