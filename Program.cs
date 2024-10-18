using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.MappingProfiles;
using Althaus_Warehouse.Mappings;
using Althaus_Warehouse.Services.Repositories;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Althaus_Warehouse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Configure the DbContext for MySQL Database
            builder.Services.AddDbContext<WarehouseDbContext>(options =>
                options.UseMySql(builder.Configuration["ConnectionStrings:WarehouseDbConnection"],
                new MySqlServerVersion(new Version(8, 0, 23))));



            //Adding Serilog for logging capabilities
            // Serilog logger configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set the minimum log level
                .WriteTo.Console() // Log to console
                .WriteTo.File("logs/warehouse_logs.txt", rollingInterval: RollingInterval.Hour) // Log to a file with Hourly rolling
                .CreateLogger();


            // Register the EmployeeRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register the ItemRepository
            builder.Services.AddScoped<IItemRepository, ItemRepository>();

            // Add AutoMapper for object mapping when making calls at endpoints instead of writing everything out I automap using mapper and my preconfigured entities and dtos
            builder.Services.AddAutoMapper(typeof(EmployeeProfile), typeof(ItemProfile));

            // Add API versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });


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

            app.MapControllerRoute(
                name: "test",
                pattern: "{controller=Test}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
