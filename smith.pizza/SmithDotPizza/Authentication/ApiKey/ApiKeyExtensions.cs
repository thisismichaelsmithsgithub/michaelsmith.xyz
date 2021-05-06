using System;
using Microsoft.AspNetCore.Authentication;
using SmithDotPizza.Configuration;

namespace SmithDotPizza.Authentication.ApiKey
{
    public static class ApiKeyExtensions
    {
        public static AuthenticationBuilder AddApiKey(
            this AuthenticationBuilder builder,
            Action<ApiKeyAuthenticationSchemeOptions> configureOptions) =>
            builder.AddScheme<ApiKeyAuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                ApiKeyDefaults.AuthenticationScheme,
                configureOptions);
    }
}