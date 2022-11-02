using MedicalOffice.Application.Dtos.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Contracts.Identity
{
    public interface IAuthService
    {
        //Task<RegistrationResponseDTO> Register(RegisterUserDTO request);

        Task<accountSatusResponseDTO> GetUserStatus([FromQuery] accountStatusRequestDTO resuest);

        Task<sendOtpResponseDTO> SendOtp(sendTotpDTO request);

        Task<AuthenticateionResponse> AuthenticateByOtp(AuthenticateByOtpRequest request);

        Task<AuthenticateionResponse> AuthenticateByPassword(authenticateByPasswordRequestDTO request);
    }
}
