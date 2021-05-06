using Microsoft.AspNetCore.Authentication;

namespace SmithDotPizza.Authentication.ApiKey
{
    public class ApiKeyAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public const string Root = "ApiKeyAuthentication";

        public string Header { get; set; }
    }
}