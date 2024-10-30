using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Services;
using Althaus_Warehouse.Services.AuthService;
using Althaus_Warehouse.Services.ItemService;
using Althaus_Warehouse.Services.Repositories;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace Althaus_Warehouse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Set up Serilog for logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/warehouse_logs.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            // Create the builder for the web application
            var builder = WebApplication.CreateBuilder(args);

            // Use Serilog for logging
            builder.Host.UseSerilog();

            // Add Problem Details service for standardized error responses
            builder.Services.AddProblemDetails();

            // Configure the DbContext for MySQL Database
            builder.Services.AddDbContext<WarehouseDbContext>(options =>
                options.UseMySql(builder.Configuration["ConnectionStrings:WarehouseDbConnection"],
                new MySqlServerVersion(new Version(8, 0, 23))));

            // Register Repositories and Services
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            // Adding authentication services
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add AutoMapper for object mapping
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            // Add API versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Adding authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireManager", policy => policy.RequireClaim("Role", "Manager"));
            });

            // Add services to the container (including MVC for views)
            builder.Services.AddControllersWithViews(); // This is crucial for Razor views

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
            //app.UseCors("AllowAll"); // Use the CORS policy
            app.UseAuthentication();
            app.UseAuthorization();

            // Routing for controllers
            app.MapControllers(); // For API controllers

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "login",
            pattern: "Auth/{controller=Login}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "employees",
                pattern: "{controller = Employees}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "items",
                pattern: "{controller = Items}/{action=Index}/{id?}");



            app.Run();
        }
    }
}
