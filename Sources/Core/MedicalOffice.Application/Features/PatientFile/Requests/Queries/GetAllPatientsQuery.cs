using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.PatientDTO;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries;

public class GetAllPatientsQuery : IRequest<List<PatientListDto>>
{
    public ListDto Dto { get; set; } = new ListDto();
}
