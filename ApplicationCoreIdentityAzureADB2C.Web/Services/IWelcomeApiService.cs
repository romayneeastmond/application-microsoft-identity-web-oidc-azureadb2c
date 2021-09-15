using ApplicationCoreIdentityAzureADB2C.Web.Models;
using System.Threading.Tasks;

namespace ApplicationCoreIdentityAzureADB2C.Web.Services
{
    public interface IWelcomeApiService
    {
        Task<WelcomeMessage> Welcome();
    }
}
