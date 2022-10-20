using MedicalOffice.Application.Models.Identity;
using MedicalOffice.WebApi.WebApi.Controllers;
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
        Task<RegistrationResponse> Register(RegistrationRequest request);

        Task<AccountSatusResponse> GetUserStatus(AccountStatusRequest resuest);

        Task<SendOtpResponse> SendOtp(SendOtpRequest request);

        Task<AuthenticateionResponse> AuthenticateByOtp(AuthenticateByOtpRequest request);

        Task<AuthenticateionResponse> AuthenticateByPassword(AuthenticateByPasswordRequest request);
    }
}
