using MediatR;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Commands
{
    public class AddOfficeCommand : IRequest<BaseResponse>
    {
        public OfficeDTO Dto { get; set; }

        public List<string> Roles { get; set; }

        public Guid UserId { get; set; }
    }
}