using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Sample.AzureKeyVault.Interfaces;

namespace Sample.AzureKeyVault.Services
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly SecretClient _secretClient;
        private readonly KeyClient _keyClient;

        public KeyVaultService(string address)
        {
            if (string.IsNullOrEmpty(address)) throw new ArgumentNullException($"Azure KeyVault's address is empty");

            _secretClient = new SecretClient(new Uri(address), new DefaultAzureCredential());

            _keyClient = new KeyClient(new Uri(address), new DefaultAzureCredential());
        }

        public async Task<KeyVaultSecret> GetSecretAsync(string name)
        {
            var response = await _secretClient
                .GetSecretAsync(name)
                .ConfigureAwait(true);

            return response?.Value;
        }

        public IList<SecretProperties> ListSecrets()
        {
            var allSecrets = _secretClient.GetPropertiesOfSecrets();

            return allSecrets?.AsEnumerable().ToList();
        }

        public async Task<bool> CreateSecretAsync(string name, string value)
        {
            var response = await _secretClient
                .SetSecretAsync(name, value)
                .ConfigureAwait(true);

            return response != null;
        }

        public async Task<bool> UpdateSecretExpireTimeAsync(string name, DateTimeOffset? utcExpireTime = null)
        {
            var key = GetSecretAsync(name)
                .ConfigureAwait(true)
                .GetAwaiter()
                .GetResult();

            key.Properties.ExpiresOn = utcExpireTime;

            var response = await _secretClient
                .UpdateSecretPropertiesAsync(key.Properties)
                .ConfigureAwait(true);

            return null != response?.Value;
        }

        public async Task<bool> DeleteSecretAsync(string name)
        {
            await _secretClient
                .StartDeleteSecretAsync(name)
                .ConfigureAwait(true);

            return true;
        }

        public async Task<KeyVaultKey> GetKeyAsync(string name)
        {
            var response = await _keyClient
                .GetKeyAsync(name)
                .ConfigureAwait(true);

            return response?.Value ?? null;
        }

        public IList<KeyProperties> ListKeys()
        {
            var allKeys = _keyClient.GetPropertiesOfKeys();

            return allKeys?.AsEnumerable().ToList();
        }

        public async Task<bool> CreateKeyAsync(string name)
        {
            var response = await _keyClient
                .CreateKeyAsync(name, KeyType.Rsa)
                .ConfigureAwait(true);

            var key = response.Value;

            return key != null;
        }

        public async Task<bool> UpdateKeyExpireTimeAsync(string name, DateTimeOffset? utcExpireTime = null)
        {
            var key = GetKeyAsync(name)
                .ConfigureAwait(true)
                .GetAwaiter()
                .GetResult();

            key.Properties.ExpiresOn = utcExpireTime;

            var response = await _keyClient
                .UpdateKeyPropertiesAsync(key.Properties)
                .ConfigureAwait(true);

            return response?.Value != null;
        }

        public async Task<bool> DeleteKeyAsync(string name)
        {
            await _keyClient
                .StartDeleteKeyAsync(name)
                .ConfigureAwait(true);

            return true;
        }
    }
}
