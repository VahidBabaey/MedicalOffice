using MediatR;
using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using OtpNet;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITotpHandler _totp;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(RoleManager<Role> roleManager, UserManager<User> userManager,
            SignInManager<User> signInManager, ITokenGenerator tokenGenerator, ITotpHandler totp)
        {
            _tokenGenerator = tokenGenerator;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _totp = totp;
        }

        public async Task<AuthenticateionResponse> AuthenticateByOtp(AuthenticateByOtpRequest request)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new Exception($"user with {request.PhoneNumber} isn't exist");

            var isVerify = _totp.Verify(request.PhoneNumber, request.Totp);

            if (!isVerify)
            {
                throw new Exception($"TotpHandler for {request.PhoneNumber} are'nt valid");
            }

            JwtSecurityToken JwtSecurityToken = await _tokenGenerator.GenerateToken(user);

            AuthenticateionResponse response = new AuthenticateionResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
            };

            return response;
        }

        public async Task<AuthenticateionResponse> AuthenticateByPassword(authenticateByPasswordRequestDTO request)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new Exception($"user with {request.PhoneNumber} isn't exist");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"credencial for {request.PhoneNumber} are'nt valid");
            }

            JwtSecurityToken JwtSecurityToken = await _tokenGenerator.GenerateToken(user);

            AuthenticateionResponse response = new AuthenticateionResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
            };

            return response;
        }

        //public async Task<UserStatusDTO> GetUserStatus(accountStatusRequestDTO request)
        //{
        //    var accountStatus = new UserStatusDTO();
        //    var user = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

        //    if (user == null)
        //    {
        //        accountStatus.Exist = false;
        //        accountStatus.LockoutEnabled = false;
        //        accountStatus.OtpOption = true;
        //        accountStatus.PasswordOption = false;
        //    }
        //    else
        //    {
        //        accountStatus.LockoutEnabled = user.LockoutEnabled;
        //        if (user.LockoutEnabled == true)
        //        {
        //            accountStatus.OtpOption = false;
        //            accountStatus.PasswordOption = false;
        //        }
        //        accountStatus.PasswordOption = user.PasswordHash == string.Empty ? false : true;
        //    }
        //    return accountStatus;
        //}

        //public Task<sendOtpResponseDTO> SendOtp(PhoneNumberDTO request)
        //{
        //    try
        //    {
        //        var totp = _totp.Generate(request.PhoneNumber);

        //        return Task.FromResult(new sendOtpResponseDTO
        //        {
        //            Message = $"the code is {totp}",
        //        });
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception($"{exception}");
        //    }
        //}

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal, string type)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var claimType = type;
            if (!principal.HasClaim(claim => claim.Type == claimType))
            {
                claimsIdentity.AddClaim(new Claim(claimType, type));
            }

            principal.AddIdentity(claimsIdentity);
            return Task.FromResult(principal);
        }
    }
}
