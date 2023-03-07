using MedicalOffice.Application.Models.EmailSetting;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}