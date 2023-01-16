﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientIllnessFormFile.Request.Command;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientIllnessFormFile.Handler.Command
{

    public class DeletePatientCommitmentFormCommandHandler : IRequestHandler<DeletePatientCommitmentFormCommand, BaseResponse>
    {
        private readonly IPatientCommitmentFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeletePatientCommitmentFormCommandHandler(IPatientCommitmentFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeletePatientCommitmentFormCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            Log log = new();

            var validationPatientCommitmentFormId = await _repository.CheckExistPatientCommitmentFormId(request.PatientCommitmentFormId);

            if (!validationPatientCommitmentFormId)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add("ID isn't exist");

                log.Type = LogType.Error;
                return response;
            }

            try
            {
                await _repository.Delete(request.PatientCommitmentFormId);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data=(new { Id = request.PatientCommitmentFormId });

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
