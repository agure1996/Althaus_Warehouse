using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.MappingProfiles;
using Althaus_Warehouse.Mappings;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

namespace Althaus_Warehouse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set up Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set the minimum log level
                .WriteTo.Console() // Log to console
                .WriteTo.File("logs/warehouse_logs.txt", rollingInterval: RollingInterval.Hour) // Log to a file with Hourly rolling
                .CreateLogger();

            // Create the builder for the web application
            var builder = WebApplication.CreateBuilder(args);

            // Use Serilog for logging
            builder.Host.UseSerilog();

            // Add services to the container
            builder.Services.AddControllersWithViews();

            // Add Problem Details service for standardized error responses
            builder.Services.AddProblemDetails();

            // Configure the DbContext for MySQL Database
            builder.Services.AddDbContext<WarehouseDbContext>(options =>
                options.UseMySql(builder.Configuration["ConnectionStrings:WarehouseDbConnection"],
                new MySqlServerVersion(new Version(8, 0, 23))));


            // Register the EmployeeRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register the ItemRepository
            builder.Services.AddScoped<IItemRepository, ItemRepository>();

            // Register the ItemTypeRepository
            builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();

            // Add AutoMapper for object mapping
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
