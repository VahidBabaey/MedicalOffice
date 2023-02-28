using MediatR;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceDurationFeature.Requests.Commands
{
    public class DeleteServiceDurationListCommand : IRequest<BaseResponse>
    {
        public ServiceDurationIdsDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}