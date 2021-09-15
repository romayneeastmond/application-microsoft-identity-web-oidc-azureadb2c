using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationCoreIdentityAzureADB2C.Web.Services
{
    public static class WelcomeApiServiceExtensions
    {
        public static void AddWelcomeApiService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IWelcomeApiService, WelcomeApiService>();
        }
    }
}
