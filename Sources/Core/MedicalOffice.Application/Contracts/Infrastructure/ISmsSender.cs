using MedicalOffice.Application.Models.Totp;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface ISmsSender
    {
        Task<bool> SendTotpSmsAsync(TotpSms totpSms);    
    }
}
