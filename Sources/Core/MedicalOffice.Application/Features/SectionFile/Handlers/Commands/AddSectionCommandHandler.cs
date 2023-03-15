using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class AddSectionCommandHandler : IRequestHandler<AddSectionCommand, BaseResponse>
{
    private readonly IValidator<AddSectionDTO> _validator;
    private readonly ISectionRepository _sectionrepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddSectionCommandHandler(IValidator<AddSectionDTO> validator, IOfficeRepository officeRepository, ISectionRepository sectionrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _validator = validator;
        _sectionrepository = sectionrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        var validationSectionName = await _sectionrepository.CheckExistSectionName(request.OfficeId, request.DTO.Name);

        if (validationSectionName)
        {
            var error = "نام بخش باید یکتا باشد.";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
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
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }
        else
        {
            try
            {
                var section = _mapper.Map<Section>(request.DTO);
                section.OfficeId = request.OfficeId;

                section = await _sectionrepository.Add(section);

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
}
