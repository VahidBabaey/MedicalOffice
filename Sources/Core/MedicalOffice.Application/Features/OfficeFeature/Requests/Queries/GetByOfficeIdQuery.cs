using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Queries
{
    public class GetByOfficeIdQuery : IRequest<BaseResponse>
    {
        public Guid officeId { get; set; }
    }
}