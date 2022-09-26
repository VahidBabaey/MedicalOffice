using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace MedicalOffice.Infrastructure.Crypto
{
    public class CryptoServiceProvider : ICryptoServiceProvider
    {
        private readonly string _secretKey;

        public CryptoServiceProvider(IOptions<CryptoSettings> cryptoSettings)
        {
            _secretKey = cryptoSettings.Value.SecretKey;
        }

        public Task<string> Decrypt(string plainText)
        {
            throw new NotImplementedException();
        }

        public Task<string> Encrypt(string plainText)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetHash(string plainText)
        {
            var hasher = SHA512.Create();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            MemoryStream stream = new(plainTextBytes);
            var hashBytes = await hasher.ComputeHashAsync(stream);
            var result = BitConverter.ToString(hashBytes).ToLower().Replace("-", string.Empty);
            return result;
        }
    }

    public static class CryptoServiceProviderRegisteration
    {
        public static IServiceCollection AddCryptography(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CryptoSettings>(configuration.GetSection("CryptoSettings"));
            services.AddTransient<ICryptoServiceProvider, CryptoServiceProvider>();

            return services;
        }
    }
}
