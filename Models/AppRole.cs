using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
            MenuItems = new HashSet<AppMenuItem>();
        }

        public AppRole(string roleName) : base(roleName)
        {
            MenuItems = new HashSet<AppMenuItem>();
        }

        public ICollection<AppMenuItem> MenuItems { get; set; }
    }
}
