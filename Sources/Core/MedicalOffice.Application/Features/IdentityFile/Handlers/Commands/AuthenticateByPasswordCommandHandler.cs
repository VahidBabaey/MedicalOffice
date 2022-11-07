using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using MedicalOffice.Application.Dtos.Identity;
using System.Net;
using System.Security.Claims;

namespace MedicalOffice.Application.Features.IdentityFile.Handlers.Commands
{
    public class AuthenticateByPasswordCommandHandler : IRequestHandler<AuthenticateByPasswordCommand, BaseCommandResponse>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public AuthenticateByPasswordCommandHandler(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ITokenGenerator tokenGenerator,
            ILogger logger,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AuthenticateByPasswordCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            AuthenticateByPasswordValidator validator = new();
            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.DTO.PhoneNumber);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = $"{_requestTitle} failed";
                    response.Errors.Add($"MedicalStaff with phone number '{request.DTO.PhoneNumber}' is't exist.");

                    log.Type = LogType.Error;
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.DTO.Password, false, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        response.Success = false;
                        response.Message = $"{_requestTitle} failed";
                        response.Errors.Add($"credencial for {request.DTO.PhoneNumber} are'nt valid");

                        log.Type = LogType.Error;
                    }
                    else
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
                                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            }
                        .Union(userClaims)
                        .Union(roleClaims);

                        JwtSecurityToken JwtSecurityToken = await _tokenGenerator.GenerateToken(user,claims);

                        AuthenticatedUserDTO authenticatedUser = _mapper.Map<AuthenticatedUserDTO>(user);
                        authenticatedUser.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);

                        response.Success = false;
                        response.Message = $"{_requestTitle} succeded";
                        response.Data.Add(authenticatedUser);

                        log.Type = LogType.Success;
                    }
                }
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
