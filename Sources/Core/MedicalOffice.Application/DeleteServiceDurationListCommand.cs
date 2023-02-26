using MediatR;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application
{
    public class DeleteServiceDurationListCommand : IRequest<BaseResponse>
    {
        public ServiceDurationIdListDTO DTO { get; set; }
        public Guid OfficeId { get; set; }
    }
}