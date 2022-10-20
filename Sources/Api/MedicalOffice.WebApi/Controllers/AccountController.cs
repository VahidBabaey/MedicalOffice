using MediatR;
using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.WebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }

        [HttpGet("status")]
        public async Task<ActionResult<AccountSatusResponse>> GetUserStatus(AccountStatusRequest request)
        {
            return Ok(await _authenticationService.GetUserStatus(request));
        }

        [HttpPost("send-otp")]
        public async Task<ActionResult<string>> SendOtp(SendOtpRequest request) 
        {
            return Ok(await _authenticationService.SendOtp(request));
        }

        [HttpPost("authenticate/otp")]
        public async Task<ActionResult<AuthenticateionResponse>> AuthenticateByOtp(AuthenticateByOtpRequest request)
        {
            return Ok(await _authenticationService.AuthenticateByOtp(request));
        }

        [HttpPost("authenticate/password")]
        public async Task<ActionResult<AuthenticateionResponse>> AuthenticateByPassword(AuthenticateByPasswordRequest request)
        {
            return Ok(await _authenticationService.AuthenticateByPassword(request));
        }

        //[HttpPatch("reset-password")]
    }
}
