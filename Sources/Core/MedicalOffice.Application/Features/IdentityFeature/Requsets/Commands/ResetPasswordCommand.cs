using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class ResetPasswordCommand : IRequest<BaseResponse>
    {
        public ResetPasswordDTO DTO { get; set; } = new ResetPasswordDTO();
    }
}