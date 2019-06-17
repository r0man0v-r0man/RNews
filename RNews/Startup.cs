using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Filters;
using RNews.Hubs;
using System;

namespace RNews
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(Configuration.GetConnectionString("RNewsDatabase")));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
                options.User.RequireUniqueEmail = true;
            })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                    options.AccessDeniedPath = "/auth/accessdenied";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "992356344313-782hen42mjrmheel8415uoe63r2tgsek.apps.googleusercontent.com";
                    options.ClientSecret = "I6o49F54Ms4CRuGutcG7LOR7";
                    options.CallbackPath = "/signin-google";
                });
            services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                    options.AccessDeniedPath = "/auth/accessdenied";
                });
            services.ConfigureExternalCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
            });
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(new ThemeActionFilter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddHttpContextAccessor();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSignalR(routes =>
            {
                routes.MapHub<GeneratePasswordHub>("/GeneratePasswordHub");
                routes.MapHub<UserPropertyHub>("/UserPropertyHub");
                routes.MapHub<UserAvatarHub>("/UserAvatarHub");
                routes.MapHub<CommentHub>("/CommentHub");
                routes.MapHub<AddCategoryHub>("/AddCategoryHub");
                routes.MapHub<RatingPostHub>("/RatingPostHub");
                routes.MapHub<CommentLikeHub>("/CommentLikeHub");
                routes.MapHub<ThemeHub>("/ThemeHub");

            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
