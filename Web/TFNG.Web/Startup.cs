﻿namespace TFNG.Web
{
    using System.Reflection;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using TFNG.Data;
    using TFNG.Data.Common;
    using TFNG.Data.Common.Repositories;
    using TFNG.Data.Models;
    using TFNG.Data.Repositories;
    using TFNG.Data.Seeding;
    using TFNG.Services.Data;
    using TFNG.Services.Data.Contracts;
    using TFNG.Services.Mapping;
    using TFNG.Services.Messaging;
    using TFNG.Web.ViewModels;
    using ТАК.Services.Messaging;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //E.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Cloudinary setup
            Account account = new Account(
                this.configuration["Cloudinary:AppName"],
                this.configuration["Cloudinary:AppKey"],
                this.configuration["Cloudinary:AppSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IAwardsService, AwardsService>();
            services.AddTransient<IDancesService, DancesService>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IGalleryService, GalleryService>();

            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("awardsRoute", "Awards", new { controller = "Awards", action = "All" });
                        endpoints.MapControllerRoute("galleryCompetitionsRoute", "Gallery/Competitions", new { controller = "Gallery", action = "AllCompetitions" });
                        endpoints.MapControllerRoute("galleryVacationsRoute", "Gallery/Vacations", new { controller = "Gallery", action = "AllVacations" });
                        endpoints.MapControllerRoute("dancesRoute", "Dances", new { controller = "Dances", action = "All" });
                        endpoints.MapControllerRoute("danceDetailsRoute", "Dances/{name}", new { Controller = "Dances", Action = "ByName" });
                        endpoints.MapControllerRoute("newsRoute", "News", new { controller = "News", action = "All" });
                        endpoints.MapControllerRoute("newsDetailsRoute", "News/{name}", new { Controller = "News", Action = "ByName" });
                        endpoints.MapControllerRoute("contactsRoute", "Contacts", new { controller = "Contacts", action = "Index" });
                        endpoints.MapControllerRoute("biographyRoute", "Biography", new { controller = "Biography", action = "Index" });
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
