using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmithDotPizza.Utilities;
using StackExchange.Redis;

namespace SmithDotPizza.Services
{
    public class UserService
    {
        private IDatabase Database { get; }

        public UserService(IDatabase database)
        {
            Database = database;
        }

        public async Task<Optional<string>> GetUserIdForApiKey(string apiKey)
        {
            var result = await Database.StringGetAsync(PrefixApiKey(apiKey));
            return result.IsNullOrEmpty
                ? Optional<string>.Empty()
                : Optional<string>.Of(result.ToString());
        }

        public async Task<IList<string>> GetRolesForUserId(string userId)
        {
            var result = await Database.SetMembersAsync(RolesKeyForUserId(userId));
            return result
                .Where(v => !v.IsNullOrEmpty)
                .Select(v => v.ToString())
                .ToList();
        }

        private static string PrefixApiKey(string apiKey) =>
            $"users/keys/{apiKey}";

        private static string PrefixUserIdKey(string userId) =>
            $"users/{userId}";

        private static string RolesKeyForUserId(string userId) =>
            $"{PrefixUserIdKey(userId)}/roles";
    }
}