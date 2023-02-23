using AutoMapper;
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
    private readonly IPatientRepository _repository;
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditPatientCommandHandler(IOfficeRepository officeRepository, IValidator<UpdatePatientDTO> validator, IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _repository = repository;
        _repositorycontact = repositorycontact;
        _repositoryaddress = repositoryaddress;
        _repositorytag = repositorytag;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditPatientCommand request, CancellationToken cancellationToken)
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

        var validationPatientId = await _repository.CheckExistPatientId(request.OfficeId, request.DTO.Id);

        if (!validationPatientId)
        {
            var error = $"ID isn't exist";
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

                await _repository.Update(patient);
                await _repositorycontact.RemovePatientContact(patient.Id);
                await _repositoryaddress.RemovePatientAddress(patient.Id);
                await _repositorytag.RemovePatientTag(patient.Id);

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
