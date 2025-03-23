using Someren_Applicatie.Repositories.Activities;
using Someren_Applicatie.Repositories.Drinks;
using Someren_Applicatie.Repositories.Lecturers;
using Someren_Applicatie.Repositories.Orders;
using Someren_Applicatie.Repositories.Rooms;
using Someren_Applicatie.Repositories.Students;

namespace Someren_Applicatie
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IStudentsRepository, DbStudentsRepository>();
            builder.Services.AddSingleton<ILecturersRepository, DbLecturersRepository>();
            builder.Services.AddSingleton<IRoomRepository, DbRoomRepository>();
            builder.Services.AddSingleton<IActivitiesRepository, DbActivitiesRepository>();
            builder.Services.AddSingleton<IDrinksRepository, DbDrinksRepository>();
            builder.Services.AddSingleton<IOrdersRepository, DbOrdersRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
}
