using MediatR;
using MedicalOffice.Application.Contracts.Identity;
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
        private readonly IOfficeRepository _officeRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOfficeRepository officeRepository, IUserOfficeRoleRepository userOfficeRoleRepository, RoleManager<Role> roleManager, UserManager<User> userManager, IOptions<JwtSettings> jwtSettings, SignInManager<User> signInManager)
        {
            _officeRepository = officeRepository;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<RegistrationResponseDTO> Register(RegistrationRequestDTO request)
        {
            var existingUser = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

            if (existingUser != null)
            {
                throw new Exception($"PhoneNumber '{request.PhoneNumber}' already exists.");
            }

            //TODO: var roleId = Environment.PatientRoleId
            var user = new User
            {
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
                ActivationStatus = UserActivationStatus.active
            };

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    var role = _roleManager.FindByNameAsync("Patient").Result;
                    var patientRole = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = "Patient"
                    };

                    if (role == null)
                    {
                        await _roleManager.CreateAsync(patientRole);

                        var newRole = await _userManager.AddToRoleAsync(user, "Patient");

                    }

                    var office = new Office
                    {
                        Id = Guid.NewGuid(),
                        Name = "selakTeb"
                    };

                    await _officeRepository.Add(office);
                    var createdUser = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);
                    var userOfficeRole = await _userOfficeRoleRepository.Add(new UserOfficeRole
                    {
                        UserId = createdUser?.Id,
                        OfficeId = office.Id,
                        RoleId = patientRole.Id
                    });

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

            var isVerify = VerifyTotp(request.PhoneNumber, request.Totp);

            if (!isVerify)
            {
                throw new Exception($"Totp for {request.PhoneNumber} are'nt valid");
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

        public async Task<accountSatusResponseDTO> GetUserStatus(accountStatusRequestDTO request)
        {
            var accountStatus = new accountSatusResponseDTO();
            var user = await _userManager.Users.SingleOrDefaultAsync(p => p.PhoneNumber == request.PhoneNumber);

            if (user == null)
            {
                accountStatus.Exist = false;
                accountStatus.ActivationStatus = UserActivationStatus.inactive;
                accountStatus.OtpOption = false;
                accountStatus.PasswordOption = false;
            }
            else
            {
                accountStatus.ActivationStatus = user.ActivationStatus;
                if (user.ActivationStatus == UserActivationStatus.inactive)
                {
                    accountStatus.OtpOption = false;
                    accountStatus.PasswordOption = false;
                }
                accountStatus.PasswordOption = user.PasswordHash == String.Empty ? false : true;
            }
            return accountStatus;
        }

        public Task<sendOtpResponseDTO> SendOtp(sendOtpRequestDTO request)
        {
            try
            {
                var totp = GenerateTotp(request.PhoneNumber);

                return Task.FromResult(new sendOtpResponseDTO
                {
                    Message = $"the code is {totp}",
                });
            }
            catch (Exception exception)
            {
                throw new Exception($"{exception}");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            List<UserOfficeRole> userOfficeRoles = await _userOfficeRoleRepository.GetByUserId(user.Id);

            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var officeClaims = new List<Claim>();
            for (int i = 0; i < userOfficeRoles.Count; i++)
            {
                officeClaims.Add(new Claim("office", userOfficeRoles[i].OfficeId.ToString()));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(officeClaims);

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

        private string GenerateTotp(string phoneNamber)
        {
            var bytes = Encoding.Default.GetBytes(phoneNamber);

            var totp = new Totp(bytes, step: 30 * 60);

            return totp.ComputeTotp(DateTime.UtcNow);
        }

        private bool VerifyTotp(string phoneNumber, string code)
        {
            var totp = new Totp(Encoding.Default.GetBytes(phoneNumber), step: 30 * 60);

            long unixTimestamp = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;

            var isVerify = totp.VerifyTotp(code, out unixTimestamp);

            return isVerify;
        }

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
