using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientDTO;
using MedicalOffice.Application.Dtos.PatientDTO.Validators;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IValidator<AddPatientDTO> _validator;
    private readonly IPatientRepository _patientrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddPatientCommandHandler(IOfficeRepository officeRepository, IValidator<AddPatientDTO> validator, IPatientRepository patientrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _patientrepository = patientrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {
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

        var isPatientExistInOffice = await _patientrepository.CheckExistByNationalId(request.DTO.NationalId, request.OfficeId);
        if (isPatientExistInOffice)
        {
            var error = "بیماری با این کد ملی قبلا در ابن مطب ثبت شده است.";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }



        var patient = _mapper.Map<Patient>(request.DTO);

        if (request.DTO.FileNumber != null)
        {
            var isFileNumberExist = await _patientrepository.IsFileNumberExist(request.DTO.FileNumber, request.OfficeId);
            if (isFileNumberExist)
            {
                var error = "این شماره پرونده قبلا ثبت شده است شماره دیگری انتخاب کنید یا مقدار آن را خالی قرار دهید تا سیستم به صورت اتوماتیک آن را تولید کند.";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            patient.FileNumber = (int)request.DTO.FileNumber;
        }

        patient.FileNumber = await _patientrepository.GenerateFileNumber();
        patient.OfficeId = request.OfficeId;
        patient = await _patientrepository.Add(patient);

        foreach (var mobile in request.DTO.PhoneNumber)
        {
            await _patientrepository.InsertContactValueOfPatientAsync(patient.Id, mobile, ContactType.Mobile);
        }
        foreach (var tel in request.DTO.TelePhoneNumber)
        {
            await _patientrepository.InsertContactValueOfPatientAsync(patient.Id, tel, ContactType.Tel);
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
}