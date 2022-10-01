using MediatR;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class EditPatientCommand : IRequest<BaseCommandResponse>
{
    public UpdateAddPatientDto Dto { get; set; } = new UpdateAddPatientDto();
}
