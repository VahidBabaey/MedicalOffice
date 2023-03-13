using MediatR;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var response = await _mediator.Send(new RegisterUserCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("register-without-password")]
        public async Task<ActionResult<Guid>> RegisterWithoutPassword(RegisterUserWithoutPassDTO dto)
        {
            var response = await _mediator.Send(new RegisterUserWithoutPassCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("send-Totp")]
        public async Task<ActionResult<string>> SendTotp(SendTotpDTO dto)
        {
            var response = await _mediator.Send(new SendTotpCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("verify-Totp")]
        public async Task<ActionResult<bool>> VerifyTotp(VerifyTotpDTO dto)
        {
            var response = await _mediator.Send(new VerifyTotpCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<UserStatusDTO>> GetUserStatus([FromQuery] UserStatusRequestDTO dto)
        {
            var response = await _mediator.Send(new GetUserStatusQuery() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("authenticate/totp")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByTotp(AuthenticateByTotpDTO dto)
        {
            var response = await _mediator.Send(new AuthenticateByTotpCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticatedUserDTO>> AuthenticateByPassword(AuthenticateByPasswordDTO dto)
        {

            var response = await _mediator.Send(new AuthenticateByPasswordCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("reset-password")]
        public async Task<ActionResult<Guid>> ResetPassword(ResetPasswordDTO dto)
        {
            var response = await _mediator.Send(new ResetPasswordCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("forget-password")]
        public async Task<ActionResult<Guid>> ForgetPassword(SetPasswordDTO dto)
        {
            var response = await _mediator.Send(new ForgetPasswordCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("set-password")]
        public async Task<ActionResult<Guid>> SetPassword([FromBody] SetPasswordDTO dto)
        {
            var response = await _mediator.Send(new SetPasswordCommand() { DTO = dto });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }

        [Authorize]
        [HttpPatch("role")]
        public async Task<ActionResult<Guid>> UpdateUserRole([FromBody] UpdateUserRoleDTO dto, [FromQuery] string officeId)
        {
            var response = await _mediator.Send(new UpdateUserRoleCommand() { DTO = dto, OfficeId = Guid.Parse(officeId) });

            return StatusCode(Convert.ToInt32(response.StatusCode), response);
        }
    }
}
