using BeFit.Data;
using BeFit.Data.Models;
using BeFit.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using BeFit.Web.Infrastructure.Extensions;
using BeFit.Web.Infrastructure.ModelBinders;

namespace BeFit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = 
                builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<BeFitDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = 
                    builder.Configuration.GetValue<bool>("Identity: SignIn.RequireConfirmedAccount");
                options.Password.RequireLowercase = 
                    builder.Configuration.GetValue<bool>("Identity: Password.RequireLowercase");
                options.Password.RequireUppercase = 
                    builder.Configuration.GetValue<bool>("Identity: Password.RequireUppercase");
                options.Password.RequireNonAlphanumeric = 
                    builder.Configuration.GetValue<bool>("Identity: Password.RequireNonAlphanumeric");
                options.Password.RequiredLength = 
                    builder.Configuration.GetValue<int>("Identity: Password.RequiredLength");
            })
                .AddEntityFrameworkStores<BeFitDbContext>();

            builder.Services.AddApplicationServices(typeof(IEventService));

            builder.Services
                .AddControllersWithViews()
                .AddMvcOptions(options =>
                {
                    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
                });

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}