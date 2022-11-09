using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterUserDTO dto)
        {
            var response = await _mediator.Send(new RegisterUserCommand() { DTO = dto });
            return Ok(response);
        }

        [HttpPost("send-Totp")]
        public async Task<ActionResult<string>> SendOtp(PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new SendTotpCommand() { DTO = dto });
            return Ok(response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<UserStatusDTO>> GetUserStatus([FromQuery] PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new GetUserStatusQuery() { DTO = dto });

            return Ok(response);
        }

        [HttpPost("authenticate/totp")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByTotp(AuthenticateByTotpDTO dto)
        {
            var response = await _mediator.Send(new AuthenticateByTotpCommand() { DTO = dto });

            return Ok(response);
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByPassword(AuthenticateByPasswordDTO dto)
        {

            var response = await _mediator.Send(new AuthenticateByPasswordCommand() { DTO = dto });

            return Ok(response);
        }

        //[HttpPatch("reset-password")]
    }
}
