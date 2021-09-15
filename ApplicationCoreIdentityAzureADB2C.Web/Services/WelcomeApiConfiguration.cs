namespace ApplicationCoreIdentityAzureADB2C.Web.Services
{
    public class WelcomeApiConfiguration : IWelcomeApiConfiguration
    {
        public string WelcomeAzureAdB2CScope { get; set; }


        public string WelcomeApiBaseAddress { get; set; }
    }
}
