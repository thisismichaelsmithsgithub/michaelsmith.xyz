using System;
using System.Threading.Tasks;
using SmithDotPizza.Utilities;
using StackExchange.Redis;

namespace SmithDotPizza.Services
{
    public class RedirectService
    {
        private IDatabase Cache { get; }
        private KeyService KeyService { get; }

        public RedirectService(
            IDatabase cache,
            KeyService keyService)
        {
            Cache = cache;
            KeyService = keyService;
        }

        public async Task<Optional<string>> GetRedirect(string key)
        {
            var result = await Cache.StringGetAsync(PrefixKey(key));
            return result.IsNullOrEmpty
                ? Optional<string>.Empty()
                : Optional<string>.Of(result.ToString());
        }

        public async Task<Optional<string>> SetRedirect(string key, Uri target)
        {
            var qualifiedKey = PrefixKey(key);
            if (await Cache.KeyExistsAsync(qualifiedKey))
            {
                return Optional<string>.Empty();
            }
            return await Cache.StringSetAsync(qualifiedKey, target.OriginalString)
                ? Optional<string>.Of(key)
                : Optional<string>.Empty();
        }

        public async Task<Optional<string>> SetRedirect(Uri target) =>
            await SetRedirect(KeyService.GenerateKey(), target);

        private static string PrefixKey(string key) => $"urls/{key}";
    }
}