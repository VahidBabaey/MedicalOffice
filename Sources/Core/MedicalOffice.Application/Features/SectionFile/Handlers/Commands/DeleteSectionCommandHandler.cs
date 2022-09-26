using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, BaseCommandResponse>
{
    private readonly ISectionRepository _repository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteSectionCommandHandler(ISectionRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseCommandResponse> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response = new();
        Log log = new();

        try
        {
            await _repository.Delete(request.SectionId);

            response.Success = true;
            response.Message = $"{_requestTitle} succeded";
            response.Data.Add(new { Id = request.SectionId });

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
        log.Messages = response.Errors;

        await _logger.Log(log);

        return response;
    }
}
