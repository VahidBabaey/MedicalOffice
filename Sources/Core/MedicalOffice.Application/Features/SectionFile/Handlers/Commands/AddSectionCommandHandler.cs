using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class AddSectionCommandHandler : IRequestHandler<AddSectionCommand, BaseResponse>
{
    private readonly IValidator<AddSectionDTO> _validator;
    private readonly ISectionRepository _repository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddSectionCommandHandler(IValidator<AddSectionDTO> validator, IOfficeRepository officeRepository, ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

        if (!validationOfficeId)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors.Add("OfficeID isn't exist");

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
        else
        {
            try
            {
                var section = _mapper.Map<Section>(request.DTO);
                section.OfficeId = request.OfficeId;

                section = await _repository.Add(section);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = section.Id });

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
