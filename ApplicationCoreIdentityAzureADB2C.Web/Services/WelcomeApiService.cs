using ApplicationCoreIdentityAzureADB2C.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApplicationCoreIdentityAzureADB2C.Web.Services
{
    public class WelcomeApiService : IWelcomeApiService
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly HttpClient _httpClient;
        private readonly string _welcomeAzureAdB2CScope = string.Empty;
        private readonly string _welcomeApiBaseAddress = string.Empty;
        private readonly IHttpContextAccessor _contextAccessor;

        public WelcomeApiService(ITokenAcquisition tokenAcquisition, HttpClient httpClient, IWelcomeApiConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _tokenAcquisition = tokenAcquisition;
            _contextAccessor = contextAccessor;
            _welcomeAzureAdB2CScope = configuration.WelcomeAzureAdB2CScope;
            _welcomeApiBaseAddress = configuration.WelcomeApiBaseAddress;
        }

        public async Task<WelcomeMessage> Welcome()
        {
            await PrepareAuthenticatedHttpClient();

            var response = await _httpClient.GetAsync($"{_welcomeApiBaseAddress}/api/members/welcome");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<WelcomeMessage>(content);

                message.Caller = _contextAccessor.HttpContext.User.Identity.Name;

                return message;
            }

            throw new HttpRequestException(response.StatusCode.ToString());
        }

        private async Task PrepareAuthenticatedHttpClient()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { _welcomeAzureAdB2CScope });

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
