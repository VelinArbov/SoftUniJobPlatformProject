namespace SoftUniJobPlatform.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;
    using Hangfire;
    using Hangfire.Dashboard;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SoftUniJobPlatform.Common;
    using SoftUniJobPlatform.Data;
    using SoftUniJobPlatform.Data.Common;
    using SoftUniJobPlatform.Data.Common.Repositories;
    using SoftUniJobPlatform.Data.Models;
    using SoftUniJobPlatform.Data.Repositories;
    using SoftUniJobPlatform.Data.Seeding;
    using SoftUniJobPlatform.Services.Data;
    using SoftUniJobPlatform.Services.Mapping;
    using SoftUniJobPlatform.Services.Messaging;
    using SoftUniJobPlatform.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
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
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(this.configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                }));

            services.AddHangfireServer();
            services.AddControllersWithViews();
            services.AddResponseCompression();
            services.AddRazorPages();

            Account account = new Account(
                this.configuration["Cloudinary:Name"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);
            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(
                serviceProvider => new SendGridEmailSender(this.configuration["SendGrid:ApiKey"]));
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICompaniesService, CompaniesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IJobsService, JobsService>();
            services.AddTransient<IApplicationUsersService, ApplicationUsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobs)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

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

            app.UseStatusCodePagesWithReExecute("/Home/HttpError?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseResponseCompression();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            if (env.IsDevelopment())
            {
                app.UseHangfireServer(new BackgroundJobServerOptions { WorkerCount = 2 });
                app.UseHangfireDashboard(
                    "/hangfire",
                    new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });
            }

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("studentsjob", "/{name:minlength(3)}", new { controller = "jobs", action = "index" });
                        endpoints.MapControllerRoute("arearoute", "{area:exists}/{controller=home}/{action=index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
                        endpoints.MapControllerRoute("students", "{controller=dashboard}/{action=applyjobasync}/{id?}", new { control = "dashboard", action = "applyjobasync" });
                        endpoints.MapRazorPages();
                    });
        }

        public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
