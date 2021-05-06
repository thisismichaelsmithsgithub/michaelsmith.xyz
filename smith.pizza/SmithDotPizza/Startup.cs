using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmithDotPizza.Authentication;
using SmithDotPizza.Authentication.ApiKey;
using SmithDotPizza.Configuration;
using SmithDotPizza.Database;
using SmithDotPizza.Services;
using StackExchange.Redis;
// ReSharper disable RedundantTypeArgumentsOfMethod

namespace SmithDotPizza
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
            services.Configure<CacheOptions>(
                Configuration.GetSection(CacheOptions.Root));
            services.Configure<ApiKeyAuthenticationSchemeOptions>(
                Configuration.GetSection(ApiKeyAuthenticationSchemeOptions.Root));
            services.Configure<KeyOptions>(
                Configuration.GetSection(KeyOptions.Root));

            services.AddSingleton<RedisConnectionManager, RedisConnectionManager>();
            services.AddScoped<IDatabase>(serviceProvider =>
                serviceProvider.GetService<RedisConnectionManager>()!.Connection.GetDatabase());

            services.AddScoped<UserService, UserService>();
            services.AddScoped<KeyService, KeyService>();
            services.AddScoped<RedirectService, RedirectService>();

            services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
                .AddApiKey(options => Configuration.Bind(ApiKeyAuthenticationSchemeOptions.Root, options));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}