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
using MedicalOffice.Application.Contracts.Persistence;
using NLog.LayoutRenderers.Wrappers;
using MedicalOffice.Application.Constants;

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
        private readonly IUserRepository _userRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;

        public AuthenticateByPasswordCommandHandler(
            IValidator<AuthenticateByPasswordDTO> validator,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ITokenGenerator tokenGenerator,
            ILogger logger,
            IMapper mapper,
            IUserRepository userRepository,
            IUserOfficeRoleRepository userOfficeRoleRepository
            )
        {
            _validator = validator;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
            _userRepository = userRepository;
            _userOfficeRoleRepository = userOfficeRoleRepository;
        }

        public async Task<BaseResponse> Handle(AuthenticateByPasswordCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var user = await _userRepository.FindExistAndActiveUser(request.DTO.PhoneNumber, isActive: true);
            //var user = await _userManager.Users.SingleOrDefaultAsync(x => x.PhoneNumber == request.DTO.PhoneNumber && x.IsActive == true);
            if (user == null)
            {
                var error = $"The User with phone number {request.DTO.PhoneNumber} is't exist!";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.NotFound, $"{_requestTitle} failed", error);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.DTO.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                var error = $"credencial for {request.DTO.PhoneNumber} is'nt valid";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            //var roles = await _userManager.GetRolesAsync(user);
            //var roleClaims = new List<Claim>();
            //for (int i = 0; i < roles.Count; i++)
            //{
            //    roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            //}

            var OfficeRoles = _userOfficeRoleRepository.GetByUserId(user.Id).Result.Select(x =>
            new OfficeRole{ OfficeId = x.OfficeId, RoleId = x.RoleId }).ToList();

            var OfficeRoleClaims = new List<Claim>();
            for (int i = 0; i < OfficeRoles.Count; i++)
            {
                OfficeRoleClaims.Add(new Claim(
                    "OfficeRole",
                    $"{OfficeRoles[i].OfficeId}|{OfficeRoles[i].RoleId}"));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())}
            .Union(userClaims)
            .Union(OfficeRoleClaims);
            //.Union(roleClaims)

            JwtSecurityToken JwtSecurityToken = await _tokenGenerator.GenerateToken(user, claims);

            AuthenticatedUserDTO authenticatedUser = _mapper.Map<AuthenticatedUserDTO>(user);
            authenticatedUser.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = authenticatedUser
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", authenticatedUser);
        }
    }
}