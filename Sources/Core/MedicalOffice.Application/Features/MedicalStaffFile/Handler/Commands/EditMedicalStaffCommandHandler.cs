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

    public class MyCommandHandlerCommandHandler : IRequestHandler<EditMedicalStaffCommand, BaseResponse>
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

        public async Task<BaseResponse> Handle(EditMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            try
            {
                //var passwordHash = await _cryptoServiceProvider.GetHash(request.DTO.PasswordHash);
                var medicalStaff = _mapper.Map<MedicalStaff>(request.DTO);
                //medicalStaff.PasswordHash = passwordHash;

                await _repository.Update(medicalStaff);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = medicalStaff.Id });
                if (request.DTO.RoleIds == null)
                {
                    throw new NullReferenceException("Role not Found");
                }
                else
                {
                    await _repository.DeleteUserOfficeRoleAsync(medicalStaff.Id);
                    foreach (var roleid in request.DTO.RoleIds)
                    {
                        await _repository.InsertToUserOfficeRole(roleid, medicalStaff.Id);
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
