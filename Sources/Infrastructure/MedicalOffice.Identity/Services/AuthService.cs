using MedicalOffice.Application.Contracts.Identity;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Models.Identity;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
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
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(RoleManager<Role> roleManager,UserManager<User> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;    
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        //public async Task<AuthResponse> Login(AuthRequest request)
        //{
        //    var user = await _userManager.FindByNameAsync(request.UserName);

        //    if (user == null)
        //    {
        //        throw new Exception($"MedicalStaff with {request.UserName} isn't exist");
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

        //    if (!result.Succeeded)
        //    {
        //        throw new Exception($"credencial for {request.UserName} are'nt valid");
        //    }

        //    JwtSecurityToken JwtSecurityToken = await GenerateToken(user);

        //    AuthResponse response = new AuthResponse
        //    {
        //        Id = user.Id.ToString(),
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        //Role = user.Role,
        //        Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
        //    };

        //    return response;
        //}


        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);
            //var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
            {
                throw new Exception($"PhoneNumber '{request.PhoneNumber}' already exists.");
            }

            //var roleId = Environment.PatientRoleId
            var user = new User
            {
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var role = _roleManager.FindByNameAsync("Patient").Result;
                    if (role==null)
                    {
                        var patientRole = await _roleManager.CreateAsync(new Role
                        {

                            Id = Guid.NewGuid(),
                            Name = "Patient"
                        });

                        var newRole = await _userManager.AddToRoleAsync(user, "Patient");
                    }

                    var createdUser = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

                    return new RegistrationResponseDTO
                    {
                        UserId = createdUser?.Id.ToString()
                    };
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
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);

            if (user == null)
                throw new Exception($"user with {request.PhoneNumber} isn't exist");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.OTP, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                throw new Exception($"credencial for {request.PhoneNumber} are'nt valid");
            }

            JwtSecurityToken JwtSecurityToken = await GenerateToken(user);

            AuthenticateionResponse response = new AuthenticateionResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,   
                Email = user.Email,
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

            JwtSecurityToken JwtSecurityToken = await GenerateToken(user);

            AuthenticateionResponse response = new AuthenticateionResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
            };

            return response;
        }

        public async Task<accountSatusResponseDTO> GetUserStatus(accountStatusRequestDTO resuest)
        {
            throw new NotImplementedException();
        }

        public async Task<sendOtpResponseDTO> SendOtp(sendOtpRequestDTO request)
        {
            string GenarateTOTP()
            {
                var bytes = Base32Encoding.ToBytes("JBSWY3DPEHPK3PXP");

                var totp = new Totp(bytes, step: 300);

                return totp.ComputeTotp(DateTime.UtcNow);
            }
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
