
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
            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.DTO.PhoneNumber && x.IsActive == true);

            if (user == null)
                user = new User();

            var isVerify = _totpHandler.Verify(request.DTO.PhoneNumber, request.DTO.Totp);
            if (!isVerify)
            {
                var error = $"Totp for {request.DTO.PhoneNumber} is'nt valid";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Union(userClaims)
                .Union(roleClaims);

                JwtSecurityToken JwtSecurityToken = await _tokenGenerator.GenerateToken(user, claims);

                AuthenticatedUserDTO authenticatedUser = _mapper.Map<AuthenticatedUserDTO>(user);
                authenticatedUser.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = authenticatedUser
                });

                return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", authenticatedUser);
            }
        }
    }
}
