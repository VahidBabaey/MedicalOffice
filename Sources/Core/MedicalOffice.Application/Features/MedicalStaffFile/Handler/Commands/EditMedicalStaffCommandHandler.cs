using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{

    public class EditMedicalStaffCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseResponse>
    {
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IUserOfficeRoleRepository _repositoryUserOfficeRole;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        private readonly string _requestTitle;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRoleRepository _medicalStaffRoleRepository;

        public EditMedicalStaffCommandHandler(
            IUserOfficeRoleRepository repositoryUserOfficeRole,
            IMedicalStaffRepository medicalStaffRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger logger,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _repositoryUserOfficeRole = repositoryUserOfficeRole;
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
            BaseResponse response = new();

            Log log = new();

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

            var existingUser = await _userRepository.GetById(existingMedicalStaff.UserId);
            if (existingUser != null)
            {
                await _userRepository.SoftDelete(existingUser.Id);
            }

            var newMedicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
            var user = new User();

            //Check user is exist
            if (request.DTO.PhoneNumber != existingMedicalStaff.PhoneNumber ||
                request.DTO.NationalID != request.DTO.NationalID)
            {
                try
                {
                    user = await _userRepository.CheckByPhoneOrNationalId(request.DTO.PhoneNumber, request.DTO.NationalID);

                    if (user != null)
                    {
                        newMedicalStaff.UserId = user.Id;
                    }
                    else
                    {
                        var newUser = _mapper.Map<User>(request.DTO);
                        newUser.Id = Guid.NewGuid();
                        newUser.UserName = request.DTO.PhoneNumber;
                        newUser.NormalizedUserName = request.DTO.PhoneNumber;

                        var userCreation = await _userManager.CreateAsync(newUser);
                        if (userCreation.Succeeded)
                        {
                            user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                        }
                        else
                        {
                            var error = $"There is a medical staff with this phoneNumber in this office";

                            await _logger.Log(new Log
                            {
                                Type = LogType.Error,
                                Header = $"{_requestTitle} failed",
                                AdditionalData = error
                            });
                            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                        }
                    }
                }
                catch (Exception err)
                {
                    var error = err.Message;
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            //update medicalStaff
            await _medicalStaffrepository.Update(newMedicalStaff);

            //Add role to user office roles
            var roleName = new List<string>();
            var userOfficeRoles = new List<UserOfficeRole>();
            var medicalstaffRole = new List<MedicalStaffRole>();
            if (request.DTO.RoleIds != null)
            {
                await _userOfficeRoleRepository.DeleteUserOfficeRoleAsync(existingMedicalStaff.UserId);

                foreach (var roleId in request.DTO.RoleIds)
                {
                    Role role = await _roleManager.FindByIdAsync(roleId.ToString());
                    if (role != null)
                    {
                        userOfficeRoles.Add(new UserOfficeRole
                        {
                            RoleId = roleId,
                            UserId = user.Id,
                            OfficeId = request.OfficeId
                        });

                        medicalstaffRole.Add(new MedicalStaffRole
                        {
                            RoleId = role.Id,
                            MedicalStaffId = newMedicalStaff.Id
                        });

                        roleName.Add(role.NormalizedName);
                    }
                }
                await _userOfficeRoleRepository.AddUserOfficeRoles(userOfficeRoles);
                await _medicalStaffRoleRepository.InsertToMedicalStaffRole(medicalstaffRole);
                await _userManager.AddToRolesAsync(user, roleName);
            }

            log.Type = LogType.Success;


            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}