using System;
using HashidsNet;
using Microsoft.Extensions.Options;
using SmithDotPizza.Configuration;

namespace SmithDotPizza.Services
{
    public class KeyService
    {
        private KeyOptions Options { get; }
        private IHashids Hashids { get; }

        public KeyService(IOptions<KeyOptions> keyOptions)
        {
            Options = keyOptions.Value;
            Hashids = new Hashids(Options.Salt);
        }

        public string GenerateKey() =>
            Hashids.EncodeLong(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
    }
}