using MediatR;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class AddPatientCommand : IRequest<BaseResponse>
{
    public AddPatientDTO DTO { get; set; } = new AddPatientDTO();
    public Guid OfficeId { get; set; }
}
