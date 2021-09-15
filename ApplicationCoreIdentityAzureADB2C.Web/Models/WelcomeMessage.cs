using System;

namespace ApplicationCoreIdentityAzureADB2C.Web.Models
{
    public class WelcomeMessage
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public string Caller { get; set; }
    }
}
