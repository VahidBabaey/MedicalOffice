﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class EditPatientCommandHandler : IRequestHandler<EditPatientCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IValidator<UpdatePatientDTO> _validator;
    private readonly IPatientRepository _patientrepository;
    private readonly IPatientContactRepository _contactrepository;
    private readonly IPatientAddressRepository _addressrepository;
    private readonly IPatientTagRepository _tagrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditPatientCommandHandler(IOfficeRepository officeRepository, IValidator<UpdatePatientDTO> validator, IPatientContactRepository contactrepository, IPatientAddressRepository addressrepository, IPatientTagRepository tagrepository, IPatientRepository patientrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _patientrepository = patientrepository;
        _contactrepository = contactrepository;
        _addressrepository = addressrepository;
        _tagrepository = tagrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditPatientCommand request, CancellationToken cancellationToken)
    {

        var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = "OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        var validationPatientId = await _patientrepository.CheckExistPatientId(request.OfficeId, request.DTO.Id);

        if (!validationPatientId)
        {
            var error = "ID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        else
        {
            try
            {
                var patient = _mapper.Map<Patient>(request.DTO);
                patient.OfficeId = request.OfficeId;

                await _patientrepository.Update(patient);
                await _contactrepository.RemovePatientContact(patient.Id);
                await _addressrepository.RemovePatientAddress(patient.Id);
                await _tagrepository.RemovePatientTag(patient.Id);

                foreach (var mobile in request.DTO.PhoneNumber)
                {
                    await _patientrepository.InsertContactValueofPatientAsync(patient.Id, mobile);
                }
                foreach (var tel in request.DTO.TelePhoneNumber)
                {
                    await _patientrepository.InsertContactValueofPatientAsync(patient.Id, tel);
                }
                foreach (var address in request.DTO.Address)
                {
                    await _patientrepository.InsertAddressofPatientAsync(patient.Id, address);
                }
                foreach (var tag in request.DTO.Tag)
                {
                    await _patientrepository.InsertTagofPatientAsync(patient.Id, tag);
                }
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = patient.Id
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", patient.Id);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
