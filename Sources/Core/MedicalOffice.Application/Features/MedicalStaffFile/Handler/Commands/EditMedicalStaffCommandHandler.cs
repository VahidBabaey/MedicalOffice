using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{

    public class EditMedicalStaffCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseResponse>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IUserOfficeRoleRepository _repositoryUserOfficeRole;
        private readonly ICryptoServiceProvider _cryptoServiceProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditMedicalStaffCommandHandler(IUserOfficeRoleRepository repositoryUserOfficeRole, IMedicalStaffRepository repository, IMapper mapper, ILogger logger, ICryptoServiceProvider cryptoServiceProvider)
        {
            _repositoryUserOfficeRole = repositoryUserOfficeRole;
            _repository = repository;
            _cryptoServiceProvider = cryptoServiceProvider;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            var validationMedicalStaffId = await _repository.CheckMedicalStaffExist(request.DTO.Id, request.OfficeId);

            if (!validationMedicalStaffId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("ID isn't exist");

                log.Type = LogType.Error;
                return response;
            }

            try
            {
                var MedicalStaff = _mapper.Map<MedicalStaff>(request.DTO);

                await _repository.Update(MedicalStaff);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data=(new { Id = MedicalStaff.Id });
                if (request.DTO.RoleIds == null)
                {
                    throw new NullReferenceException("Role not Found");
                }
                else
                {
                    await _repository.DeleteUserOfficeRoleAsync(MedicalStaff.Id);
                    foreach (var roleid in request.DTO.RoleIds)
                    {
                        //await _officeRepository.InsertToUserOfficeRole(roleid, MedicalStaff.Id);
                    }
                }

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}