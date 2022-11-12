using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    public class AuthenticateByPasswordCommand : IRequest<BaseResponse>
    {
        public AuthenticateByPasswordDTO DTO { get; set; } = new();
    }
}