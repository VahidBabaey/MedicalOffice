using MediatR;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class VerifyTotpCommand : IRequest<BaseResponse>
    {
        public VerifyTotpDTO DTO { get; set; }
    }
}