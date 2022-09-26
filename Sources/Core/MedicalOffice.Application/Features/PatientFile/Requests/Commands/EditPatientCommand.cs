using MediatR;
using MedicalOffice.Application.Dtos.Patient;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class EditPatientCommand : IRequest<BaseCommandResponse>
{
    public PazireshDTO Dto { get; set; } = new PazireshDTO();
}
