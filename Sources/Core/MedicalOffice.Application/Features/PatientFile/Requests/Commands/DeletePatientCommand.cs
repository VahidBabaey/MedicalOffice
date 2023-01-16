using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Commands;

public class DeletePatientCommand : IRequest<BaseResponse>
{
    public Guid PatientId { get; set; }
    public Guid OfficeId { get; set; }
}
