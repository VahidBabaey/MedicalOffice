﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO;
using MedicalOffice.Application.Features.Experiment.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.Experiment.Handlers.Commands
{

    public class EditExperimentCommandHandler : IRequestHandler<EditExperimentCommand, BaseCommandResponse>
    {
        private readonly IExperimentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditExperimentCommandHandler(IExperimentRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditExperimentCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                var experiment = _mapper.Map<ExperimentPre>(request.DTO);

                await _repository.Update(experiment);

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";
                response.Data.Add(new { Id = experiment.Id });

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
