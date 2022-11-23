using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands
{
    public class AuthenticateByTotpCommand : IRequest<BaseResponse>
    {
        public AuthenticateByTotpDTO Dto { get; set; } = new();
    }
}