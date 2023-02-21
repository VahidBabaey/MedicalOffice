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
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class EditSectionCommandHandler : IRequestHandler<EditSectionCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IValidator<UpdateSectionDTO> _validator;
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditSectionCommandHandler(IOfficeRepository officeRepository, IValidator<UpdateSectionDTO> validator, ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditSectionCommand request, CancellationToken cancellationToken)
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

        var validationSectionId = await _repository.CheckExistSectionId(request.OfficeId, request.DTO.Id);

        if (!validationSectionId)
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

        try
        {
            var section = _mapper.Map<Section>(request.DTO);
            section.OfficeId = request.OfficeId;

            await _repository.Update(section);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = section.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", section.Id);
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
