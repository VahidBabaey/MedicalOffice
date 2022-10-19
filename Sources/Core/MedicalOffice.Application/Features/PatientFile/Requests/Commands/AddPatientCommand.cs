using MediatR;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class AddPatientCommand : IRequest<BaseCommandResponse>
{
    public PatientDTO Dto { get; set; } = new PatientDTO();
}
