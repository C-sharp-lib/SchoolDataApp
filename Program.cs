using Microsoft.EntityFrameworkCore;
using SchoolMVCApp.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<SchoolMVCAppDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();
        //ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContext<TodoContext>(options => options.UseMySQL(Configuration["DBInfo:ConnectionString"]));
        //    services.AddMvc();
        //    services.AddSession();
        //}

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}