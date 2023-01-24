using MediatR;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.OfficeFeature.Requests.Commands
{
    public class AddOfficeWithoutSpecificPermissionCommand : IRequest<BaseResponse>
    {
        public List<UserOfficeDTO> DTO { get; set; }
    }
}