using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmithDotPizza.Services;

namespace SmithDotPizza.Authentication.ApiKey
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationSchemeOptions>
    {
        private UserService UserService { get; }

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            UserService userService)
            : base(options, logger, encoder, clock)
        {
            UserService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.Header))
            {
                return AuthenticateResult.Fail("");
            }

            var apiKey = Request.Headers[Options.Header];
            var userId = await UserService.GetUserIdForApiKey(apiKey);

            if (!userId.HasValue)
            {
                return AuthenticateResult.Fail("");
            }

            var roles = await UserService.GetRolesForUserId(userId.Value);
            var claims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            var identity = new ClaimsIdentity(claims);
            return AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(identity),
                    Scheme.Name));
        }
    }
}