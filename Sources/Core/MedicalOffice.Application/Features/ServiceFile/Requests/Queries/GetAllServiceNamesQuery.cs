using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries
{
    public class GetAllServiceNamesQuery : IRequest<BaseResponse>
    {
        public Guid OfficeId { get; set; }
    }
}