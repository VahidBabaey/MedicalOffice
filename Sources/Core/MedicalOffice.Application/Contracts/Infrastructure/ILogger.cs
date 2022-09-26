using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public interface ILogger
{
    Task Log(Log log);
}
