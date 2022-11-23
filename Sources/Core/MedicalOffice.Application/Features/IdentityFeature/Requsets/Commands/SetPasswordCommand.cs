using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class SetPasswordCommand : IRequest<BaseResponse>
    {
        public SetPasswordDTO Dto { get; set; }
    }
}
