using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.IdentityFile.Requsets.Commands
{
    public class AuthenticateByTotpCommand : IRequest<BaseCommandResponse>
    {
        public AuthenticateByTotpDTO DTO { get; set; } = new();
    }
}