using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Patient;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries;

public class GetPatientByFileNumberQuery : IRequest<List<PatientListDto>>
{
    public ListDto Dto { get; set; } = new ListDto();
    public string FileNumber { get; set; } = string.Empty;
}
