using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Patient;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries;

public class GetPatientByNationalCodeQuery : IRequest<List<PatientListDto>>
{
    public ListDto Dto { get; set; } = new ListDto();
    public string NationalCode { get; set; } = string.Empty;
}
