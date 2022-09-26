using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class EditSectionCommandHandler : IRequestHandler<EditSectionCommand, BaseCommandResponse>
{
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditSectionCommandHandler(ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseCommandResponse> Handle(EditSectionCommand request, CancellationToken cancellationToken)
    {
        BaseCommandResponse response = new();

        Log log = new();

        try
        {
            var section = _mapper.Map<Section>(request.Dto);

            await _repository.Update(section);

            response.Success = true;
            response.Message = $"{_requestTitle} succeded";
            response.Data.Add(new { Id = section.Id });

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
