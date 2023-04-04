using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetPatientInsuranceInquireQuery : IRequest<BaseResponse>
    {
        public string NationalId { get; set; }
        public Guid OfficeId { get; set; }
    }
}