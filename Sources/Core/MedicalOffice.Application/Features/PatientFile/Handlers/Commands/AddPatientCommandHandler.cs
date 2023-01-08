﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, BaseResponse>
{
    private readonly IValidator<PatientDTO> _validator;
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddPatientCommandHandler(IValidator<PatientDTO> validator, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {

        BaseResponse response = new();

        Log log = new();

        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            log.Type = LogType.Error;
        }
        else
        {
            try
            {
                var patient = _mapper.Map<Patient>(request.DTO);

                patient = await _repository.Add(patient);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = patient.Id });
                if (request.DTO.PhoneNumber == null)
                {

                }
                else
                {
                    foreach (var mobile in request.DTO.PhoneNumber)
                    {
                        await _repository.InsertContactValueofPatientAsync(patient.Id, mobile);
                    }
                    foreach (var address in request.DTO.Address)
                    {
                        await _repository.InsertAddressofPatientAsync(patient.Id, address);
                    }
                    foreach (var tag in request.DTO.Tag)
                    {
                        await _repository.InsertTagofPatientAsync(patient.Id, tag);
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
        }

        log.Header = response.StatusDescription;
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }


}









