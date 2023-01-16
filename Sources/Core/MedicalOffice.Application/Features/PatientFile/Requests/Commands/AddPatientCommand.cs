using MediatR;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class AddPatientCommand : IRequest<BaseResponse>
{
    public PatientDTO DTO { get; set; } = new PatientDTO();
    public Guid OfficeId { get; set; }
}
