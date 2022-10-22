﻿using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Identity.Model;
using MedicalOffice.WebApi.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<User> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new Exception($"MedicalStaff with {request.UserName} isn't exist");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"credencial for {request.UserName} are'nt valid");
            }

            JwtSecurityToken JwtSecurityToken = await GenerateToken(user);

            AuthResponse response = new AuthResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                Email = user.Email,
                //Role = user.Role,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
            };

            return response;
        }


        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p=>p.PhoneNumber == request.PhoneNumber); 
            //var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"PhoneNumber '{request.PhoneNumber}' already exists.");
            }

            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "");
                    return new RegistrationResponse();
                }
                else
                {
                    var errors = string.Join(",", result.Errors.Select(x => $"{x.Code} - {x.Description}"));
                    throw new Exception(errors);
                }
            }
            else
            {
                throw new Exception($"UserName {request.Email} already exists.");
            }
        }

        public async Task<AuthenticateionResponse> AuthenticateByOtp(AuthenticateByOtpRequest request)
        {
            // How to generate token for that purpose?
            // How to add a token provider?
            // How to shit?

            var user = _userManager.Users.SingleOrDefault(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                return await Task.FromResult(new AuthenticateionResponse { Success = false });

            var tokenIsValid = _userManager.VerifyUserTokenAsync(user, "how-to-name?", "authenticate-by-phonenumber", request.OTP).Result;

            return await Task.FromResult(new AuthenticateionResponse { Success = tokenIsValid });
        }

        public async Task<AuthenticateionResponse> AuthenticateByPassword(AuthenticateByPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountSatusResponse> GetUserStatus(AccountStatusRequest resuest)
        {
            throw new NotImplementedException();
        }

        public async Task<SendOtpResponse> SendOtp(SendOtpRequest request)
        {
            throw new NotImplementedException();
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

    }
}
