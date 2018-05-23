using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Library.Models.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Library
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:LibraryBooks:ConnectionString"]);
                options.EnableSensitiveDataLogging();
            });
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:LibraryIdentity:ConnectionString"]));
            //services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IdentityRole, AppRole>();
            services.AddIdentity<IdentityUser, AppRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>()
                    .AddDefaultTokenProviders();
            services.AddTransient<IMenuManager, MenuManager>();
            services.AddTransient<IBookRepository, EFBookRepository>();
            services.AddTransient<IGenreRepository, EFGenreRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddTransient<IUserBookRepository, EFUserBookRepository>();
            services.AddTransient<IReturnRequestRepository, EFReturnRequestRepository>();
            services.AddTransient<ICartLineRepository, EFCartLineRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{genreId:int}/Books/Page{bookPage:int}",
                    defaults: new { Controller = "Book", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "Page{bookPage:int}",
                    defaults: new { controller = "Book", action = "List", bookPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "{genreId:int}",
                    defaults: new { controller = "Book", action = "List", bookPage = 1 });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { controller = "Book", action = "List", bookPage = 1 });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Book}/{action=List}/{id?}");
            });

            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);

        }
    }
}
