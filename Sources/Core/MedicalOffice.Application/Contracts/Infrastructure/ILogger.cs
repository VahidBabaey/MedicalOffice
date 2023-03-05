using MedicalOffice.Application.Models.Logger;

namespace MedicalOffice.Application.Contracts.Infrastructure;

public interface ILogger
{
    Task Log(Log log);
}
