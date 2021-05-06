using System;
using Microsoft.Extensions.Options;
using SmithDotPizza.Configuration;
using StackExchange.Redis;

namespace SmithDotPizza.Database
{
    public class RedisConnectionManager
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;
        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        public RedisConnectionManager(IOptions<CacheOptions> cacheOptions)
        {
            var connection = cacheOptions.Value.Connection;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(
                () => ConnectionMultiplexer.Connect(connection));
        }

    }
}