using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{
    public class EditMedicalStaffCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseResponse>
    {
        private readonly IValidator<UpdateMedicalStaffDTO> _validator;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly string _requestTitle;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;

        public EditMedicalStaffCommandHandler(
            IValidator<UpdateMedicalStaffDTO> validator,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _validator = validator;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            //Validate
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            //Check staff is exist
            var existingMedicalStaff = await _medicalStaffrepository.GetById(request.DTO.Id);
            if (existingMedicalStaff == null)
            {
                var error = $"The medicalStaff isn't exist";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var newMedicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            newMedicalStaff.UserId = existingMedicalStaff.UserId;
            newMedicalStaff.OfficeId = request.OfficeId;

            //Check user is exist or not
            var user = new User();
            if (request.DTO.PhoneNumber != existingMedicalStaff.PhoneNumber ||
                request.DTO.NationalID != existingMedicalStaff.NationalID)
            {
                var existingUserByPhoneNumber = await _userRepository.GetUserByPhoneNumber(request.DTO.PhoneNumber);
                var existingUserByNationalId = await _userRepository.GetUserByNationalId(request.DTO.NationalID);

                if (existingUserByPhoneNumber != null && existingUserByNationalId != null)
                {
                    switch (existingUserByNationalId.Id == existingUserByNationalId.Id)
                    {
                        case true:
                            var isStaffExistingStaffInOffice = await _medicalStaffrepository.CheckExistByOfficeIdAndPhoneNumber(request.OfficeId, request.DTO.PhoneNumber);
                            if (isStaffExistingStaffInOffice)
                            {
                                await _logger.Log(new Log
                                {
                                    Type = LogType.Error,
                                    Header = $"{_requestTitle} failed",
                                    AdditionalData = "There is a medical staff with this phoneNumber in this office"
                                });
                                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                                    "There is a medical staff with this phoneNumber in this office");
                            }

                            user = existingUserByPhoneNumber;
                            break;

                        case false:
                            var error = $"PhoneNumber: '{request.DTO.PhoneNumber}' or nationalId: '{request.DTO.NationalID}' already exists.";
                            await _logger.Log(new Log
                            {
                                Type = LogType.Error,
                                Header = $"{_requestTitle} failed",
                                AdditionalData = error
                            });
                            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                    }
                }

                var invalidPhoneAndNationalId = (existingUserByPhoneNumber != null && existingUserByNationalId == null) || (existingUserByPhoneNumber == null && existingUserByNationalId != null);
                if (invalidPhoneAndNationalId)
                {
                    var error = $"PhoneNumber: '{request.DTO.PhoneNumber}' or nationalId: '{request.DTO.NationalID}' already exists.";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                var noUserExist = existingUserByPhoneNumber == null && existingUserByNationalId == null;
                if (noUserExist)
                {
                    var newUser = _mapper.Map<User>(request.DTO);
                    newUser.Id = Guid.NewGuid();
                    newUser.UserName = request.DTO.PhoneNumber;
                    newUser.NormalizedUserName = request.DTO.PhoneNumber;

                    var userCreation = await _userManager.CreateAsync(newUser);
                    if (!userCreation.Succeeded)
                    {
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = userCreation.Errors.Select(x => x.Description)
                        }); ;
                        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", userCreation.Errors.Select(x => x.Description).ToArray());
                    }

                    user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                }
                newMedicalStaff.UserId = user.Id;
            }

            //update medicalStaff
            await _medicalStaffrepository.Patch(existingMedicalStaff, newMedicalStaff, true);

            //Add role to user office roles
            await _userOfficeRoleRepository.Add(new UserOfficeRole
            {
                RoleId = request.DTO.RoleId,
                UserId = user.Id,
                OfficeId = request.OfficeId
            });

            #region RemoveOldStaffPermissions
            await _userOfficePermissionRepository.SoftDeleteRange(request.OfficeId, user.Id);
            #endregion

            #region AddNewPermissions
            var permissions = await _rolePermissionRepository.GetByRoleId(request.DTO.RoleId);
            var userOfficePermissions = new List<UserOfficePermission>();
            foreach (var item in permissions)
            {
                userOfficePermissions.Add(new UserOfficePermission
                {
                    UserId = user.Id,
                    OfficeId = request.OfficeId,
                    PermissionId = item.Id
                });
            }
            await _userOfficePermissionRepository.AddRange(userOfficePermissions)
            #endregion
;
            Role role = await _roleManager.FindByIdAsync(request.DTO.RoleId.ToString());
            if (role != null)
            {
                await _userManager.AddToRoleAsync(user, role.NormalizedName);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = existingMedicalStaff.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", existingMedicalStaff.Id);
        }
    }
}