using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class ForgetPasswordCommand : IRequest<BaseResponse>
    {
        public SetPasswordDTO DTO { get; set; }
    }
}