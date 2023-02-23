using AutoMapper;
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
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IValidator<PatientDTO> _validator;
    private readonly IPatientRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddPatientCommandHandler(IOfficeRepository officeRepository, IValidator<PatientDTO> validator, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddPatientCommand request, CancellationToken cancellationToken)
    {

        BaseResponse response = new();

        var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = $"OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = response.Errors
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
                AdditionalData = response.Errors
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        else
        {
            try
            {
                var patient = _mapper.Map<Patient>(request.DTO);
                patient.OfficeId = request.OfficeId;

                patient = await _repository.Add(patient);

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









