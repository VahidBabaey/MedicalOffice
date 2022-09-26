using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail(Email email);
}