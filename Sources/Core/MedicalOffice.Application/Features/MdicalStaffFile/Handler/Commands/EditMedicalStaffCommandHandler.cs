﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MdicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MdicalStaffFile.Handler.Commands
{

    public class MyCommandHandlerCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseCommandResponse>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IUserOfficeRoleRepository _repositoryuserofficerole;
        private readonly ICryptoServiceProvider _cryptoServiceProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public MyCommandHandlerCommandHandler(IUserOfficeRoleRepository repositoryuserofficerole, IMedicalStaffRepository repository, IMapper mapper, ILogger logger, ICryptoServiceProvider cryptoServiceProvider)
        {
            _repositoryuserofficerole = repositoryuserofficerole;
            _repository = repository;
            _cryptoServiceProvider = cryptoServiceProvider;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                var passwordHash = await _cryptoServiceProvider.GetHash(request.DTO.PasswordHash);
                var medicalstaff = _mapper.Map<MedicalStaff>(request.DTO);
                medicalstaff.PasswordHash = passwordHash;

                await _repository.Update(medicalstaff);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = medicalstaff.Id });
                if (request.DTO.RoleIds == null)
                {

                }
                else
                {
                    await _repository.DeleteUserOfficeRoleAsync(medicalstaff.Id);
                    foreach (var roleid in request.DTO.RoleIds)
                    {
                        await _repository.InserttoUserOfficeRoleAsync(roleid, medicalstaff.Id);
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

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
