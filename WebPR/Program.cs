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

            // ��������� ������
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // ������������� ����� ����� ������
                options.Cookie.HttpOnly = true; // ������������� ���� HttpOnly ��� ������������
                options.Cookie.IsEssential = true; // ����� ������ ���� �������� ���� ��� �������� �� cookies
            });

            // Add services to the container.
            builder.Services.AddRazorPages();

            // ��������� ApplicationDbContext � �������������� ������ ����������� �� appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ��������� IHttpContextAccessor ��� �������� � PageModel
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

            // ��������� ��������� ������
            app.UseSession();

            app.UseAuthorization();

			app.MapRazorPages();

			app.Run();
		}
	}
}
