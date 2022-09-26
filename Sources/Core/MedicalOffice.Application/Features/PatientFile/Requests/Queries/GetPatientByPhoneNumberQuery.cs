using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Patient;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries;

public class GetPatientByPhoneNumberQuery : IRequest<List<PatientListDto>>
{
    public ListDto Dto { get; set; } = new ListDto();
    public string PhoneNumber { get; set; } = string.Empty;
}
