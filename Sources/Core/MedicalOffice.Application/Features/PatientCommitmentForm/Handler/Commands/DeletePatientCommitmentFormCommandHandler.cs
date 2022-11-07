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

    public class DeletePatientCommitmentFormCommandHandler : IRequestHandler<DeletePatientCommitmentFormCommand, BaseCommandResponse>
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

        public async Task<BaseCommandResponse> Handle(DeletePatientCommitmentFormCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            Log log = new();

            try
            {
                await _repository.Delete(request.PatientCommitmentFormId);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = request.PatientCommitmentFormId });

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
