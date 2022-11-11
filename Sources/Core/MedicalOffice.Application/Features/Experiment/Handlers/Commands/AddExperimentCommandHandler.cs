﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ExperimentDTO.Validators;
using MedicalOffice.Application.Dtos.Identity.Validators;
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

    public class AddExperimentCommandHandler : IRequestHandler<AddExperimentCommand, BaseCommandResponse>
    {
        private readonly IExperimentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddExperimentCommandHandler(IExperimentRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddExperimentCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddExperimentValidator validator = new();

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
                    var experiment = _mapper.Map<ExperimentPre>(request.DTO);

                    experiment = await _repository.Add(experiment);

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
            }

            log.Header = response.Message;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
