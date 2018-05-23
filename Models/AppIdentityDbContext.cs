using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser, AppRole, string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        //public DbSet<AppMenuItem> AppMenuItem { get; set; }
    }
}
