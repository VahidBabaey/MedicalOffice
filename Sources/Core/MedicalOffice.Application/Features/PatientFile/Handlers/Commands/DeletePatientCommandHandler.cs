using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.PatientFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.PatientFile.Handlers.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, BaseCommandResponse>
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

    public async Task<BaseCommandResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response = new();
        Log log = new();

        try
        {
            await _repository.Delete(request.PatientId);
            await _repositorycontact.Delete(request.PatientId);
            await _repositoryaddress.Delete(request.PatientId);
            await _repositorytag.Delete(request.PatientId);
            response.Success = true;
            response.Message = $"{_requestTitle} succeded";
            response.Data.Add(new { Id = request.PatientId });

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
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }
}
