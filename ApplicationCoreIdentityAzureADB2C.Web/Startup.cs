using ApplicationCoreIdentityAzureADB2C.Web.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace ApplicationCoreIdentityAzureADB2C.Web
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
            services.AddDistributedMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddOptions();

            services.AddMicrosoftIdentityWebAppAuthentication(Configuration, Constants.AzureAdB2C)
                    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { Configuration["WelcomeApi:WelcomeAzureAdB2CScope"] })
                    .AddInMemoryTokenCaches();

            services.AddSingleton<IWelcomeApiConfiguration>(x =>
                new WelcomeApiConfiguration
                {
                    WelcomeAzureAdB2CScope = Configuration["WelcomeApi:WelcomeAzureAdB2CScope"],
                    WelcomeApiBaseAddress = Configuration["WelcomeApi:WelcomeApiBaseAddress"]
                }
            );

            services.AddWelcomeApiService(Configuration);

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();

            services.AddOptions();

            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
