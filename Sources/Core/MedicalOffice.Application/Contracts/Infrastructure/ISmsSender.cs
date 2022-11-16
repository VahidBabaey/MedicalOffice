using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface ISmsSender
    {
        Task<bool> SendTotpSmsAsync(TotpSms totpSms);    
    }
}
