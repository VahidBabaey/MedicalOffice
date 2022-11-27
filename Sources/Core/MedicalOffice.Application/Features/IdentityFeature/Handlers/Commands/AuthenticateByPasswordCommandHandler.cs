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
using FluentValidation;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class AuthenticateByPasswordCommandHandler : IRequestHandler<AuthenticateByPasswordCommand, BaseResponse>
    {
        private readonly IValidator<AuthenticateByPasswordDTO> _validator;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public AuthenticateByPasswordCommandHandler(
            IValidator<AuthenticateByPasswordDTO> validator,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ITokenGenerator tokenGenerator,
            ILogger logger,
            IMapper mapper)
        {
            _validator = validator;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AuthenticateByPasswordCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new BaseResponse();
            Log log = new();

            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.Dto.PhoneNumber);
                if (user == null)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add($"User with phone number '{request.Dto.PhoneNumber}' is't exist.");

                    log.Type = LogType.Error;
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Dto.Password, false, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        response.Success = false;
                        response.StatusDescription = $"{_requestTitle} failed";
                        response.Errors.Add($"credencial for {request.Dto.PhoneNumber} are'nt valid");

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
                        response.StatusDescription = $"{_requestTitle} succeded";
                        response.Data = (authenticatedUser);

                        log.Type = LogType.Success;
                    }
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
