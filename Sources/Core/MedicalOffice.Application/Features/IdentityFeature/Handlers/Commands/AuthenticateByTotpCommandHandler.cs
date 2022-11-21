
using AutoMapper;
using FluentValidation;
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
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace MedicalOffice.Application.Features.IdentityFeature.Handlers.Commands
{
    public class AuthenticateByTotpCommandHandler : IRequestHandler<AuthenticateByTotpCommand, BaseResponse>
    {
        private readonly IValidator<AuthenticateByTotpDTO> _validator;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ITotpHandler _totpHandler;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public AuthenticateByTotpCommandHandler(
            IValidator<AuthenticateByTotpDTO> validator,
            UserManager<User> userManager,
            ITokenGenerator tokenGenerator,
            ITotpHandler totpHandler,
            ILogger logger,
            IMapper mapper)
        {
            _validator = validator;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _totpHandler = totpHandler;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(AuthenticateByTotpCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new BaseResponse();
            Log log = new();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
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
                        response.StatusDescription = $"{_requestTitle} failed";
                        response.Errors.Add($"User with phone number '{request.DTO.PhoneNumber}' is't exist.");

                        log.Type = LogType.Error;
                    }
                    else if (await _userManager.IsLockedOutAsync(user))
                    {
                        // User exists but can not login until _userManager.GetLockoutEndDateAsync()
                    }
                    else
                    {
                        var isVerify = _totpHandler.Verify(request.DTO.PhoneNumber, request.DTO.Totp);

                        if (!isVerify)
                        {
                            response.StatusCode = HttpStatusCode.NotAcceptable;
                            response.Success = false;
                            response.StatusDescription = $"{_requestTitle} failed";
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
                                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
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
                            response.StatusDescription = $"{_requestTitle} succeded";
                            response.Data = (authenticatedUser);

                            log.Type = LogType.Success;
                        }
                    }
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode.Unauthorized;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
