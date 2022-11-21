using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        private new ObjectResult Response(BaseResponse response)
        {
            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterUserDTO dto)
        {
            var response = await _mediator.Send(new RegisterUserCommand() { DTO = dto });

            return Response(response);
        }

        [HttpPost("send-Totp")]
        public async Task<ActionResult<string>> SendOtp(PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new SendTotpCommand() { DTO = dto });

            return Response(response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<UserStatusDTO>> GetUserStatus([FromQuery] PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new GetUserStatusQuery() { DTO = dto });

            return Response(response);
        }

        [HttpPost("authenticate/totp")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByTotp(AuthenticateByTotpDTO dto)
        {
            var response = await _mediator.Send(new AuthenticateByTotpCommand() { DTO = dto });

            return Response(response);
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByPassword(AuthenticateByPasswordDTO dto)
        {

            var response = await _mediator.Send(new AuthenticateByPasswordCommand() { DTO = dto });

            return Response(response);
        }

        //[Authorize]
        [HttpPatch("reset-password")]
        public async Task<ActionResult<Guid>> ResetPassword(ResetPasswordDTO dto)
        {
            var response = await _mediator.Send(new ResetPasswordCommand() { DTO = dto });

            return Response(response);
        }

        //[Authorize]
        [HttpPatch("set-password")]
        public async Task<ActionResult<Guid>> SetPassword([FromBody]SetPasswordDTO dto)
        {
            var response = await _mediator.Send(new SetPasswordCommand() { DTO = dto });

            return Response(response);
        }

    }
}
