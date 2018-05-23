using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(
                    new Genre { Title = "Компьютерная литература" },
                    new Genre { Title = "Роман" },
                    new Genre { Title = "Сказка" }
                );
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                     new Book { Title = "ASP.NET Core MVC", Author = "Фримен А.", Genre = context.Genres.Where(g=>g.Title == "Компьютерная литература").FirstOrDefault() },
                     new Book { Title = "Идиот", Author = "Достоевский", Genre = context.Genres.Where(g => g.Title == "Роман").FirstOrDefault() },
                     new Book { Title = "Алиса в Зазеркалье", Author = "Льюис Кэрролл", Genre = context.Genres.Where(g => g.Title == "Сказка").FirstOrDefault() },
                     new Book { Title = "Гарри Поттер", Author = "Джоан Роулинг", Genre = context.Genres.Where(g => g.Title == "Роман").FirstOrDefault() },
                     new Book { Title = "Pro AngularJS", Author = "Фримен А.", Genre = context.Genres.Where(g => g.Title == "Компьютерная литература").FirstOrDefault() },
                     new Book { Title = "Бесы", Author = "Достоевский", Genre = context.Genres.Where(g => g.Title == "Роман").FirstOrDefault() },
                     new Book { Title = "Война и мир", Author = "Толстой Л.Н.", Genre = context.Genres.Where(g => g.Title == "Роман").FirstOrDefault() },
                     new Book { Title = "Золотой ключик, или Приключения Буратино", Author = "Толстой А.Н.", Genre = context.Genres.Where(g => g.Title == "Сказка").FirstOrDefault() },
                     new Book { Title = "Приключения Чиполлино", Author = "Джанни Родари", Genre = context.Genres.Where(g => g.Title == "Сказка").FirstOrDefault() },
                     new Book { Title = "Библия C#", Author = "Фленов М.Е.", Genre = context.Genres.Where(g => g.Title == "Компьютерная литература").FirstOrDefault() },
                     new Book { Title = "Библия Delphi", Author = "Фленов М.Е.", Genre = context.Genres.Where(g => g.Title == "Компьютерная литература").FirstOrDefault() },
                     new Book { Title = "CLR VIA C#", Author = "Джеффри Рихтер", Genre = context.Genres.Where(g => g.Title == "Компьютерная литература").FirstOrDefault() },
                     new Book { Title = "Поющие в терновнике", Author = "Колин Маккалоу", Genre = context.Genres.Where(g => g.Title == "Роман").FirstOrDefault() },
                     new Book { Title = "Современные операционные системы", Author = "Таненбаум Э.", Genre = context.Genres.Where(g => g.Title == "Компьютерная литература").FirstOrDefault() }
                );
                context.SaveChanges();
            }
        }
    }
}