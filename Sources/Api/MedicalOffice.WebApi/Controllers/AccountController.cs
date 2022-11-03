using MediatR;
using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Features.IdentityFile.Requsets.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authenticationService;
        public AccountController(IAuthService authenticationService, IMediator mediator)
        {
            _mediator = mediator;
            _authenticationService = authenticationService;
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
            //return Ok(await _authenticationService.GetUserStatus(dto));
        }

        [HttpPost("authenticate/otp")]
        public async Task<ActionResult<AuthenticateionResponse>> AuthenticateByOtp(AuthenticateByOtpRequest request)
        {
            return Ok(await _authenticationService.AuthenticateByOtp(request));
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticateionResponse>> AuthenticateByPassword(authenticateByPasswordRequestDTO request)
        {
            return Ok(await _authenticationService.AuthenticateByPassword(request));
        }

        //[HttpPatch("reset-password")]
    }
}
