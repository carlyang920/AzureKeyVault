using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.AzureKeyVault.Interfaces;
using Sample.AzureKeyVault.Services;

namespace Sample.AzureKeyVault.Test.Services
{
    [TestClass]
    public class KeyVaultServiceTest
    {
        private readonly IKeyVaultService _service;

        public KeyVaultServiceTest()
        {
            _service = new KeyVaultService("[Key Vault Address]");
        }

        [TestMethod]
        public void TestGetSecretAsync()
        {
            var secretName = $"{DateTime.Now:yyyyMMddHHmmssfff}-SecretTest";

            try
            {
                _service.CreateSecretAsync(
                        secretName,
                        Guid.NewGuid().ToString()
                    )
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                var secret =
                    _service.GetSecretAsync(secretName)
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult();

                Assert.IsTrue(!string.IsNullOrEmpty(secret.Value));
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteSecretAsync(secretName)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestListSecrets()
        {
            try
            {
                var result = _service.ListSecrets();

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestCreateSecretAsync()
        {
            var secretName = $"{DateTime.Now:yyyyMMddHHmmssfff}-SecretTest";

            try
            {
                Assert.IsTrue(
                    _service.CreateSecretAsync(
                            secretName,
                            Guid.NewGuid().ToString()
                        )
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult()
                );
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteSecretAsync(secretName)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestUpdateSecretExpireDateAsync()
        {
            var secretName = $"{DateTime.Now:yyyyMMddHHmmssfff}-SecretTest";

            try
            {
                var result = _service.CreateSecretAsync(
                        secretName,
                        Guid.NewGuid().ToString()
                    )
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                if (result)
                {
                    result = _service.UpdateSecretExpireTimeAsync(secretName, DateTimeOffset.UtcNow.AddSeconds(10.0))
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult();
                }

                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteSecretAsync(secretName)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestDeleteSecretAsync()
        {
            var secretName = $"{DateTime.Now:yyyyMMddHHmmssfff}-SecretTest";

            try
            {
                _service.CreateSecretAsync(
                        secretName,
                        Guid.NewGuid().ToString()
                    )
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                _service.DeleteSecretAsync(secretName)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void TestGetKeyAsync()
        {
            var name = $"{DateTime.Now:yyyyMMddHHmmssfff}-KeyTest";

            try
            {
                var result = _service.CreateKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                if (result)
                {
                    var key = _service.GetKeyAsync(name)
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult();

                    result = null != key;
                }

                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestListKeys()
        {
            try
            {
                var result = _service.ListKeys();

                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestCreateKeyAsync()
        {
            var name = $"{DateTime.Now:yyyyMMddHHmmssfff}-KeyTest";

            try
            {
                var result = _service.CreateKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestUpdateKeyExpireDateAsync()
        {
            var name = $"{DateTime.Now:yyyyMMddHHmmssfff}-KeyTest";

            try
            {
                var result = _service.CreateKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                if (result)
                {
                    result = _service.UpdateKeyExpireTimeAsync(name, DateTimeOffset.UtcNow.AddSeconds(10.0))
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult();
                }

                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
            finally
            {
                _service.DeleteKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        [TestMethod]
        public void TestDeleteKeyAsync()
        {
            var name = $"{DateTime.Now:yyyyMMddHHmmssfff}-KeyTest";

            try
            {
                var result = _service.CreateKeyAsync(name)
                    .ConfigureAwait(true)
                    .GetAwaiter()
                    .GetResult();

                if (result)
                {
                    result = _service.DeleteKeyAsync(name)
                        .ConfigureAwait(true)
                        .GetAwaiter()
                        .GetResult();
                }

                Assert.IsTrue(result);
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{e}");
                Assert.IsTrue(false);
            }
        }
    }
}
