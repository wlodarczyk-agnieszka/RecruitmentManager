using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecruitmentManager.ApplicationDbContext;


namespace RecruitmentManager
{
    public class Startup
    {
        protected IConfigurationRoot Configuration;

        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddXmlFile("appsettings.xml", false, true);
            Configuration = configurationBuilder.Build();
        }

        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             string connString = Configuration["ConnectionString"];
            
            services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer(connString));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            //services.AddAuthentication().AddGoogle(googleOptions =>
            //    {
            //        googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //        googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //    });

            //services.AddAuthentication().AddLinkedIn(options =>
            //{
            //    options.ClientId = Configuration["Authentication:LinkedIn:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:LinkedIn:ClientSecret"];
            //});

            services.AddSingleton<UtilityClasses.UtilDate>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default2",
                    template: "{controller=Home}/{action=Index}");
                routes.MapRoute(
                    name: "default1",
                    template: "{controller=Manager}/{action=Index}/{id}");
                routes.MapRoute(
                    name: "default3",
                    template: "{controller=Manager}/{action=ViewOffer}/{id}");
                routes.MapRoute(
                    name: "default4",
                    template: "{controller=Manager}/{action=RemoveNote}/{id}");
                routes.MapRoute(
                    name: "default5",
                    template: "{controller=Manager}/{action=Search}/{text}");
                
            });
        }
    }
}
