using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class DeletePatientCommand : IRequest<BaseCommandResponse>
{
    public Guid PatientId { get; set; }
}
