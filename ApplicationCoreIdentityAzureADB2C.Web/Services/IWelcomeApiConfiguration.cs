namespace ApplicationCoreIdentityAzureADB2C.Web.Services
{
    public interface IWelcomeApiConfiguration
    {
        string WelcomeAzureAdB2CScope { get; }


        string WelcomeApiBaseAddress { get; }
    }
}
