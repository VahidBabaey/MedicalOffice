
using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Dtos.IdentityDTO.Validators;
using MedicalOffice.Application.Features.IdentityFeature.Requsets.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class AuthenticateByTotpCommandHandler : IRequestHandler<AuthenticateByTotpCommand, BaseCommandResponse>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public AuthenticateByTotpCommandHandler(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ITokenGenerator tokenGenerator,
            ITotpHandler totpHandler,
            ILogger logger,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _totpHandler = totpHandler;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseCommandResponse> Handle(AuthenticateByTotpCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            AuthenticateByTotpValidator validator = new();
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
                try
                {
                    var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.DTO.PhoneNumber);
                    if (user == null)
                    {
                        response.Success = false;
                        response.Message = $"{_requestTitle} failed";
                        response.Errors.Add($"MedicalStaff with phone number '{request.DTO.PhoneNumber}' is't exist.");

                        log.Type = LogType.Error;
                    }
                    else if (await _userManager.IsLockedOutAsync(user))
                    {
                        // MedicalStaff exists but can not login until _userManager.GetLockoutEndDateAsync()
                    }
                    else
                    {
                        var isVerify = _totpHandler.Verify(request.DTO.PhoneNumber, request.DTO.Totp);

                        if (!isVerify)
                        {
                            response.Success = false;
                            response.Message = $"{_requestTitle} failed";
                            response.Errors.Add($"Totp for {request.DTO.PhoneNumber} are'nt valid");

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

                            //AuthenticatedUserDTO authenticatedUser = new()
                            //{
                            //    Id = user.Id.ToString(),
                            //    UserName = user.UserName,
                            //    FirstName = user.FirstName,
                            //    LastName = user.LastName,
                            //    Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken)
                            //};

                            response.Success = true;
                            response.Message = $"{_requestTitle} succeded";
                            response.Data.Add(authenticatedUser);

                            log.Type = LogType.Success;
                        }
                    }
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.Message = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
