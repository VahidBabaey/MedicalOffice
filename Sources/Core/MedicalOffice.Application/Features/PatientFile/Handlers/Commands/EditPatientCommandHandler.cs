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
    private readonly IValidator<UpdatePatientDTO> _validator;
    private readonly IPatientRepository _repository;
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditPatientCommandHandler(IValidator<UpdatePatientDTO> validator, IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IPatientRepository repository, IMapper mapper, ILogger logger)
    {
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
                patient.OfficeId = request.OfficeId;

                await _repository.Update(patient);
                await _repositorycontact.RemovePatientContact(patient.Id);
                await _repositoryaddress.RemovePatientAddress(patient.Id);
                await _repositorytag.RemovePatientTag(patient.Id);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
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
                response.StatusCode = HttpStatusCode.BadRequest;
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
