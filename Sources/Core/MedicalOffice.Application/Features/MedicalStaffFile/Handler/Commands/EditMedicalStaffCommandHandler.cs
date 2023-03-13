using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{
    public class EditMedicalStaffCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseResponse>
    {
        private readonly IValidator<UpdateMedicalStaffDTO> _validator;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUserOfficePermissionRepository _userOfficePermissionRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public EditMedicalStaffCommandHandler(
            IValidator<UpdateMedicalStaffDTO> validator,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IUserRepository userRepository,
            IRolePermissionRepository rolePermissionRepository,
            IUserOfficePermissionRepository userOfficePermissionRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _validator = validator;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _medicalStaffrepository = medicalStaffRepository;
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _userOfficePermissionRepository = userOfficePermissionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            #region Validate
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            #endregion

            #region CheckStaffExist
            var existingMedicalStaff = await _medicalStaffrepository.GetExistingStaffById(request.DTO.Id, request.OfficeId);
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
            #endregion

            //Check user is exist
            var user = new User();
            var userByPhoneNumber = new User();
            var userByNationalId = new User();

            if (existingMedicalStaff.PhoneNumber == request.DTO.PhoneNumber && existingMedicalStaff.NationalId == request.DTO.NationalId)
            {
                user = existingMedicalStaff.User;
            }
            else
            {
                userByPhoneNumber = await _userRepository.GetUserByPhoneNumber(request.DTO.PhoneNumber);
                userByNationalId = await _userRepository.GetUserByNationalId(request.DTO.NationalId);

                //condistions
                var bothNotExist = userByPhoneNumber == null && userByNationalId == null;
                var bothExist = userByPhoneNumber != null && userByNationalId != null;
                var phoneNumberExist = userByPhoneNumber != null || userByNationalId == null;
                var nationalIdExist = userByPhoneNumber == null || userByNationalId != null;

                if (bothNotExist)
                {
                    var newUser = _mapper.Map<User>(request.DTO);
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

                    user = await _userRepository.GetById(newUser.Id);
                }

                else if (bothExist)
                {
                    if (userByPhoneNumber.Id == userByNationalId.Id)
                    {
                        user = userByPhoneNumber;
                    }
                    else
                    {
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = "شماره تماس و کد ملی به کاربران دیگیری متعلق است."
                            //AdditionalData = "This phone and nationalId is belong to other users"
                        });
                        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", "شماره تماس و کد ملی به کاربران دیگیری متعلق است.");
                    }
                }

                else if (phoneNumberExist)
                {
                    var hasPassWord = await _userManager.HasPasswordAsync(userByPhoneNumber);
                    if (hasPassWord)
                    {
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = "این شماره تماس به کاربر دیگری متعلق است."
                            //AdditionalData = "This phone is belonged to another user"
                        });
                        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", "این شماره تماس به کاربر دیگری متعلق است.");
                    }
                    else
                    {
                        userByPhoneNumber.NationalId = request.DTO.NationalId;
                        await _userManager.UpdateAsync(userByPhoneNumber);

                        user = userByPhoneNumber;
                    }
                }

                else if (nationalIdExist)
                {
                    var hasPassWord = await _userManager.HasPasswordAsync(userByNationalId);
                    if (hasPassWord)
                    {
                        await _logger.Log(new Log
                        {
                            Type = LogType.Error,
                            Header = $"{_requestTitle} failed",
                            AdditionalData = "این کدملی به کاربر دیگری متعلق است"
                            //AdditionalData = "This nationalId is belonged to another user"
                        });
                        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", "این کدملی به کاربر دیگری متعلق است");
                    }
                    else
                    {
                        userByNationalId.PhoneNumber = request.DTO.PhoneNumber;
                        userByNationalId.UserName = request.DTO.PhoneNumber;
                        userByNationalId.NormalizedUserName = request.DTO.PhoneNumber;
                        await _userManager.UpdateAsync(userByNationalId);

                        user = userByNationalId;
                    }
                }
            }

            var newMedicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            newMedicalStaff.OfficeId = request.OfficeId;
            newMedicalStaff.UserId = user.Id;
            newMedicalStaff.User = user;

            #region UpdateMedicalStaff
            await _medicalStaffrepository.Patch(existingMedicalStaff, newMedicalStaff, false);
            #endregion

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
            await _userOfficePermissionRepository.AddRange(userOfficePermissions);
            #endregion

            #region RemoveUserOfficeRole
            var userOfficeRole = await _userOfficeRoleRepository.GetByUserAndOfficeId(existingMedicalStaff.UserId, request.OfficeId);
            if (userOfficeRole != null)
            {
                foreach (var item in userOfficeRole)
                {
                    await _userOfficeRoleRepository.SoftDelete(item.Id);
                }
            }
            #endregion

            #region AddRoleToUser
            await _userOfficeRoleRepository.Add(new UserOfficeRole
            {
                RoleId = request.DTO.RoleId,
                UserId = user.Id,
                OfficeId = request.OfficeId
            });

            var role = await _roleManager.FindByIdAsync(request.DTO.RoleId.ToString());
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
            #endregion
        }
    }
}