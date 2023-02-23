using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IPatientRepository _repository;
    private readonly IPatientContactRepository _repositorycontact;
    private readonly IPatientAddressRepository _repositoryaddress;
    private readonly IPatientTagRepository _repositorytag;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeletePatientCommandHandler(IOfficeRepository officeRepository, IPatientContactRepository repositorycontact, IPatientAddressRepository repositoryaddress, IPatientTagRepository repositorytag, IPatientRepository repository, ILogger logger)
    {
        _officeRepository = officeRepository;
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

        var validationPatientId = await _repository.CheckExistPatientId(request.OfficeId, request.PatientId);

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

        try
        {
            await _repository.SoftDelete(request.PatientId);
            await _repositorycontact.RemovePatientContact(request.PatientId);
            await _repositoryaddress.RemovePatientAddress(request.PatientId);
            await _repositorytag.RemovePatientTag(request.PatientId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");
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
