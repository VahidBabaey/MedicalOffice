using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Dtos.LoginDTO;
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

        [HttpPost("user-exist/mobile-phone")]
        public async Task<ActionResult<UserExistenceResponseDTO>> UserExistenceByMobilePhone(MobilePhoneExistenceRequestDTO request)
        {
            return Ok(await _authenticationService.UserExistenceByMobilePhone(request));
        }

        [HttpPost("user-exist/national-code")]
        public async Task<ActionResult<UserExistenceResponseDTO>> UserExistenceByNationalCode(NationalIDExistenceRequestDTO request)
        {
            return Ok(await _authenticationService.UserExistenceByNationalCode(request));
        }

        [HttpPost("login/mobile-phone")]
        public async Task<ActionResult<LoginResponseDTO>> LoginByMobilePhone(LoginByMobilePhoneDTO request)
        {
            return Ok(await _authenticationService.LoginByMobilePhone(request));
        }

        [HttpPost("login/national-code")]
        public async Task<ActionResult<LoginResponseDTO>> LoginByNationalCode(LoginByNationalIdDTO request)
        {
            return Ok(await _authenticationService.LoginByNationalCode(request));
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }
    }
}
