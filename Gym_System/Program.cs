using Gym_System.Data;
using Gym_System.Models;
using Gym_System.Repository;
using Gym_System.Repository.Gym_System.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gym_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.d
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IMemberSessionRepository, MemberSessionRepository>();
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<ISessionTypeRepository, SessionTypeRepository>();
            builder.Services.AddScoped<IVisitorSessionRepository, VisitorSessionRepository>();
            builder.Services.AddDbContext<GymDbContext>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<GymDbContext>()
                .AddDefaultTokenProviders();

            // ? езого MVC (Controllers + Views)
            builder.Services.AddControllersWithViews();


            var app = builder.Build();

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
                pattern: "{controller=Account}/{action=Login}/{id?}");


            app.Run();

        
            

        }
    }
}