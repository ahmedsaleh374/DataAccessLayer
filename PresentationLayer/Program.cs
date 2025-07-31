using BusinessLogicLayer.Profiles;
using BusinessLogicLayer.Services.Classes;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Context;
using DataAccessLayer.Models.IdentityModels;
using DataAccessLayer.Repository.Classes;
using DataAccessLayer.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //register dbcontext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            //register identity dbcontext
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            #region option for identity 
                //    (options => 
                //{
                //    options.Password.RequireLowercase=true;
                //    options.Password.RequireUppercase=true;
                //    options.User.RequireUniqueEmail=true;
                //}) 
            #endregion
                .AddEntityFrameworkStores<ApplicationDbContext>() 
                .AddDefaultTokenProviders();

            //register repo
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            

            // register unit of work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //register service
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            //register auto mapper
            builder.Services.AddAutoMapper(x => x.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(x => x.AddProfile(new DepartmentProfile()));

            // ACCESS DENIED
            builder.Services.ConfigureApplicationCookie(option => 
            {
                option.LoginPath = "/Account/Login";
                option.LogoutPath = "/Account/LogOut";
                option.AccessDeniedPath = "/Account/AccessDenied";
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStatusCodePagesWithReExecute("/Home/NotFound");
            

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
