using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetPatientFileNumberQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
    }
}