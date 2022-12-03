using MediatR;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceDurationScheduling.Requests.Commands
{
    public class AddServiceDurationCommand : IRequest<BaseResponse>
    {
        public ServiceDurationDTO Dto { get; set; }

        public Guid OfficeId{ get; set; }
    }
}