using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries;

public class GetAllPatientsQuery : IRequest<BaseResponse>
{
    public ListDto Dto { get; set; } = new ListDto();
    public Guid OfficeId { get; set; }
}
