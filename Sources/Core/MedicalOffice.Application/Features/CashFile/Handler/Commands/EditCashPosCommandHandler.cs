using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class EditCashPosCommandHandler : IRequestHandler<EditCashPosCommand, BaseResponse>
{
    private readonly IValidator<UpdateCashPosDTO> _validator;
    private readonly ICashPosRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public EditCashPosCommandHandler(IValidator<UpdateCashPosDTO> validator, ICashPosRepository repository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(EditCashPosCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

        bool isreceptionIdExist = await _repository.CheckExistReceptionId(request.DTO.OfficeId, request.DTO.ReceptionId);
        bool iscashIdExist = await _repository.CheckExistCashId(request.DTO.OfficeId, request.DTO.CashId);

        if (!isreceptionIdExist || !iscashIdExist)
        {
            List<string> errors = new List<string>();
            var error = $"اطلاعات وارد شده صحیح نمیباشد.";
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            errors = new List<string> { error };
            response.Errors = errors;

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
                var cashpos = _mapper.Map<CashPos>(request.DTO);

                await _repository.Update(cashpos);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = cashpos.Id });

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
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

