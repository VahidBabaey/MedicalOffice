using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Commands
{

    public class DeleteMedicalStaffCommandHandler : IRequestHandler<DeleteMedicalStaffCommand, BaseResponse>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteMedicalStaffCommandHandler(IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IMedicalStaffRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteMedicalStaffCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            var validationMedicalStaffId = await _repository.CheckMedicalStaffExist(request.MedicalStaffId, request.OfficeId);

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
                await _repository.DeleteUserOfficeRoleAsync(request.MedicalStaffId);
                await _repository.Delete(request.MedicalStaffId);
                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data=(new { Id = request.MedicalStaffId });

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
