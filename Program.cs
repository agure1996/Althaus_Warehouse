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
            var builder = WebApplication.CreateBuilder(args);


            // Set up Serilog for logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/warehouse_logs.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();


            // Use Serilog for logging
            builder.Host.UseSerilog();

            // Configure the DbContext for MySQL Database
            builder.Services.AddDbContext<WarehouseDbContext>(options =>
                options.UseMySql(builder.Configuration["ConnectionStrings:WarehouseDbConnection"],
                new MySqlServerVersion(new Version(8, 0, 23))));

            // Register repositories and services
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemTypeRepository, ItemTypeRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Add AutoMapper for object mapping
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configure API versioning
            builder.Services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

                // Customizing the challenge response for different endpoints
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        // Prevent default response for authorization challenges
                        context.HandleResponse();

                        // Redirect only non-API requests to homepage
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";
                            context.Response.WriteAsync("{\"error\": \"Unauthorized\"}");
                        }
                        else
                        {
                            context.Response.Redirect("/Home/Index");
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        // Only redirect non-API requests to homepage on forbidden access
                        if (!context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.Redirect("/Home/Index");
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";
                            context.Response.WriteAsync("{\"error\": \"Forbidden\"}");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            // Add authorization policy for API controllers
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireManager", policy => policy.RequireClaim("Role", "Manager"));
            });

            // Add services for both API and MVC controllers
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Middleware pipeline
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            // Route API controllers with /api prefix
            app.MapControllers();

            // MVC routing configuration
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Custom route for specific MVC areas
            app.MapControllerRoute(
                name: "login",
                pattern: "Auth/{controller=Login}/{action=Index}/{id?}");

            // Additional routes for specific controllers as needed
            app.MapControllerRoute(
                name: "employees",
                pattern: "{controller=Employees}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "items",
                pattern: "{controller=Items}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
