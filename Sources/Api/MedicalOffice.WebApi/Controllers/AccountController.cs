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
        public async Task<ActionResult<Guid>> Register(RegisterUserDTO request)
        {
            var response = await _mediator.Send(new RegisterUserCommand() { Dto = request });
            return Ok(response);
        }

        [HttpPost("send-Totp")]
        public async Task<ActionResult<string>> SendOtp(sendTotpDTO request)
        {
            var response = await _mediator.Send(new SendTotpCommand() { Dto = request });
            return Ok(response);
        }

        [HttpGet("status")]
        public async Task<ActionResult<accountSatusResponseDTO>> GetUserStatus([FromQuery] accountStatusRequestDTO request)
        {
            return Ok(await _authenticationService.GetUserStatus(request));
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
