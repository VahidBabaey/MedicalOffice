using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Requests.Queries
{
    public class GetPatietByIdQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
        public PatientIdDTO DTO { get; set; }
    }
}