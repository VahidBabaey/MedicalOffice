using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO.Validators;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{

    public class AddMedicalStaffCommandHandler : IRequestHandler<AddMedicalStaffCommand, BaseCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IMedicalStaffRepository _medicalStaffrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddMedicalStaffCommandHandler(RoleManager<Role> roleManager, UserManager<User> userManager, IMedicalStaffRepository medicalStaffrepository, IMapper mapper, ILogger logger, IUserOfficeRoleRepository userOfficeRoleRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _medicalStaffrepository = medicalStaffrepository;
            _mapper = mapper;
            _logger = logger;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddMedicalStaffValidator validator = new();

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
                    var user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                    if (user == null)
                    {
                        user = _mapper.Map<User>(request.DTO);
                        user.UserName = request.DTO.PhoneNumber;
                        user.EmailConfirmed = true;
                        var userCreation = await _userManager.CreateAsync(user);
                        if (userCreation.Succeeded)
                            user = await _userManager.FindByNameAsync(request.DTO.PhoneNumber);
                    }

                    var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
                    medicalStaff.UserId = user.Id;
                    medicalStaff = await _medicalStaffrepository.Add(medicalStaff);

                    response.Success = true;
                    response.Message = $"{_requestTitle} succeded";
                    response.Data.Add(new { Id = medicalStaff.Id });

                    if (request.DTO.RoleIds != null)
                    {
                        foreach (var roleId in request.DTO.RoleIds)
                        {
                            Role role = await _roleManager.FindByIdAsync(roleId.ToString());
                            if (role != null)
                            {
                                await _userOfficeRoleRepository.InsertToUserOfficeRole(roleId, medicalStaff.Id, request.DTO.OfficeId);

                                await _userManager.AddToRoleAsync(user, role.Name);
                            }
                        }
                    }
                    log.Type = LogType.Success;
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
