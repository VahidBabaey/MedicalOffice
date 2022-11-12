﻿using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, BaseResponse>
{
    private readonly IPatientRepository _repository;
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeletePatientCommandHandler(IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IPatientRepository repository, ILogger logger)
    {
        _repository = repository;
        _repositorycontact = repositorycontact;
        _repositoryaddress = repositoryaddress;
        _repositorytag = repositorytag;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();
        Log log = new();

        try
        {
            await _repository.Delete(request.PatientId);
            await _repositorycontact.Delete(request.PatientId);
            await _repositoryaddress.Delete(request.PatientId);
            await _repositorytag.Delete(request.PatientId);
            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = request.PatientId });

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
