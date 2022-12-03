using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(RegisterUserDTO dto)
        {
            var response = await _mediator.Send(new RegisterUserCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("send-Totp")]
        public async Task<ActionResult<string>> SendOtp(PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new SendTotpCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<UserStatusDTO>> GetUserStatus([FromQuery] PhoneNumberDTO dto)
        {
            var response = await _mediator.Send(new GetUserStatusQuery() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("authenticate/totp")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByTotp(AuthenticateByTotpDTO dto)
        {
            var response = await _mediator.Send(new AuthenticateByTotpCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByPassword(AuthenticateByPasswordDTO dto)
        {

            var response = await _mediator.Send(new AuthenticateByPasswordCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("reset-password")]
        public async Task<ActionResult<Guid>> ResetPassword(ResetPasswordDTO dto)
        {
            var response = await _mediator.Send(new ResetPasswordCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("set-password")]
        public async Task<ActionResult<Guid>> SetPassword([FromBody]SetPasswordDTO dto)
        {
            var response = await _mediator.Send(new SetPasswordCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize(Roles ="SuperAdmin")]
        [HttpPatch("role")]
        public async Task<ActionResult<Guid>> UpdateUserRole([FromBody] UserRoleDTO dto)
        {
            var response = await _mediator.Send(new UpdateUserRoleCommand() { Dto = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
