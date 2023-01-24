using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Commands
{
    public class AddOfficeWithoutSpecificPermissionCommandHandler : IRequestHandler<AddOfficeWithoutSpecificPermissionCommand, BaseResponse>
    {
        private readonly IValidator<UserOfficeDTO> _validator;
        private readonly UserManager<User> _userManager;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly string _requestTitle;

        public AddOfficeWithoutSpecificPermissionCommandHandler(
            IValidator<UserOfficeDTO> validator,
            UserManager<User> userManager,
            IRoleRepository roleRepository,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IOfficeRepository officeRepository,
            ILogger logger,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userManager = userManager;
            _validator = validator;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _officeRepository = officeRepository;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddOfficeWithoutSpecificPermissionCommand request, CancellationToken cancellationToken)
        {
            var result = new List<Guid>();
            foreach (var item in request.DTO)
            {
                var validationResult = await _validator.ValidateAsync(item, cancellationToken);
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

                var existingUser = await _userManager.FindByIdAsync(item.UserId.ToString());
                if (existingUser == null)
                {
                    var error = $"User with id:{item.UserId} isn't exist";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return ResponseBuilder.Faild(HttpStatusCode.NotFound,
                        $"{_requestTitle} failed",
                        error);
                }

                var isOfficeExist = await _officeRepository.GetByTelePhoneNumber(item.TelePhoneNumber);

                if (isOfficeExist)
                {
                    var error = "An office with this phone number exist";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return ResponseBuilder.Faild(HttpStatusCode.Conflict,
                        $"{_requestTitle} failed",
                        error);
                }

                var newOffice = await _officeRepository.Add(_mapper.Map<Office>(item));

                var roleName = _roleRepository.GetById(item.UserRole).Result.NormalizedName;
                if (roleName == null)
                {
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = $"{item.UserRole} isn't exit"
                    });

                    return ResponseBuilder.Faild(
                        HttpStatusCode.BadRequest,
                        $"{_requestTitle} failed",
                        $"{item.UserRole} isn't exit");
                }

                var userRole = _userManager.GetUsersInRoleAsync(roleName).Result.SingleOrDefault(x=>x.Id==item.UserId);
                if (userRole == null)
                {
                    await _userManager.AddToRoleAsync(existingUser, roleName);
                }

                var userOfficeRoles = await _userOfficeRoleRepository.Add(new UserOfficeRole
                {
                    UserId = item.UserId,
                    RoleId = item.UserRole,
                    OfficeId = newOffice.Id,
                });

                result.Add(newOffice.Id);
            }


            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = result
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", result);
        }
    }
}
