using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eventures.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Eventures.Data.Models;
using Eventures.Middleware;
using Eventures.Services.Contracts;
using Eventures.Services;
using Microsoft.Extensions.Logging;
using Eventures.Data.Common;
using AutoMapper;
using Eventures.Cloud;
using Eventures.AutoMapper;
using Eventures.Cloud.Contracts;

namespace Eventures
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole>>();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });
            services.AddHttpsRedirection(options => 
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 44379;
            });
            services.AddMvc(opt =>
                            opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(opt =>
            {
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Name = "SID";
                opt.IdleTimeout = new System.TimeSpan(0, 2, 0);
            });
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Application services
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<ICloudService, CloudService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped(typeof(ConnectToCloud));
            
            //services.AddSingleton<ILogger, MyFileLogger>();
          
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddFile("Logs/myapp-{Date}.txt");
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
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMiddleware<SeedRolesMiddleware>();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
