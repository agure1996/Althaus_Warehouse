using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Services;
using Althaus_Warehouse.Services.AuthService;
using Althaus_Warehouse.Services.Repositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

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

            // Register IEmployeeService
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            // Add AutoMapper for object mapping
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //adding authentication services
            builder.Services.AddScoped<IAuthService, AuthService>();



            // Add API versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            //adding authorisation
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });


            // Configure JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"],
                    ValidAudience = builder.Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
                };
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
            app.UseAuthentication();
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
