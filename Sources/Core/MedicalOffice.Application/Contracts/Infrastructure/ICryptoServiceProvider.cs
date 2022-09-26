namespace MedicalOffice.Application.Contracts.Infrastructure;

public interface ICryptoServiceProvider
{
    Task<string> GetHash(string plainText);
    Task<string> Encrypt(string plainText);
    Task<string> Decrypt(string plainText);
}
