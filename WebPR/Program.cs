using Microsoft.EntityFrameworkCore;
using WebPR.Data;
using Microsoft.AspNetCore.Http;

namespace WebPR
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            // Добавляем сессии
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // Устанавливаем время жизни сессии
                options.Cookie.HttpOnly = true; // Устанавливаем флаг HttpOnly для безопасности
                options.Cookie.IsEssential = true; // Чтобы сессия была доступна даже без согласия на cookies
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Добавляем ApplicationDbContext с использованием строки подключения из appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Добавляем IHttpContextAccessor для инъекции в PageModel
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

            // Добавляем поддержку сессий
            app.UseSession();

            app.UseAuthorization();

			app.MapRazorPages();

			app.Run();
		}
	}
}
