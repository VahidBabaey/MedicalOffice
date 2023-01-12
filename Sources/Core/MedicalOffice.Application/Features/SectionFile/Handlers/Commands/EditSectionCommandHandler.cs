using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class EditSectionCommandHandler : IRequestHandler<EditSectionCommand, BaseResponse>
{
    private readonly IValidator<UpdateSectionDTO> _validator;
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditSectionCommandHandler(IValidator<UpdateSectionDTO> validator, ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditSectionCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        var validationSectionId = await _repository.CheckExistSectionId(request.OfficeId, request.DTO.Id);

        if (!validationSectionId)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors.Add("ID isn't exist");

            log.Type = LogType.Error;
            return response;
        }

        var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            log.Type = LogType.Error;
        }

        try
        {
            var section = _mapper.Map<Section>(request.DTO);
            section.OfficeId = request.OfficeId;

            await _repository.Update(section);

            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = section.Id });

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
