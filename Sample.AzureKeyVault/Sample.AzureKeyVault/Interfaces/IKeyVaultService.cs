using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;

namespace Sample.AzureKeyVault.Interfaces
{
    public interface IKeyVaultService
    {
        Task<KeyVaultSecret> GetSecretAsync(string name);
        IList<SecretProperties> ListSecrets();
        Task<bool> CreateSecretAsync(string name, string value);
        Task<bool> UpdateSecretExpireTimeAsync(string name, DateTimeOffset? utcExpireTime = null);
        Task<bool> DeleteSecretAsync(string name);

        Task<KeyVaultKey> GetKeyAsync(string name);
        IList<KeyProperties> ListKeys();
        Task<bool> CreateKeyAsync(string name);
        Task<bool> UpdateKeyExpireTimeAsync(string name, DateTimeOffset? utcExpireTime = null);
        Task<bool> DeleteKeyAsync(string name);
    }
}
