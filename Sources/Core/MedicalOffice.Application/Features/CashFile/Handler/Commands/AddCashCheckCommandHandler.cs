﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class AddCashCheckCommandHandler : IRequestHandler<AddCashCheckCommand, BaseResponse>
{
    private readonly IValidator<CashCheckDTO> _validator;
    private readonly ICashCheckRepository _cashcheckrepository;
    private readonly ICashRepository _cashrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashCheckCommandHandler(ICashRepository cashrepository, IValidator<CashCheckDTO> validator, ICashCheckRepository cashcheckrepository, IMapper mapper, ILogger logger)
    {
        _validator = validator;
        _cashrepository = cashrepository;
        _cashcheckrepository = cashcheckrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashCheckCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        Log log = new();

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
                var cash = await _cashrepository.AddCashForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.Cost);

                var cashcheck = _cashcheckrepository.AddCashCheckForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, cash, request.DTO.Cost, request.DTO.BankId);

                response.Success = true;
                response.StatusCode = HttpStatusCode.OK;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = cashcheck.Id });

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

