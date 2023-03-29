﻿using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class AddCashCheckCommandHandler : IRequestHandler<AddCashCheckCommand, BaseResponse>
{
    private readonly IValidator<CashCheckDTO> _validator;
    private readonly ICashCheckRepository _cashcheckrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashCheckCommandHandler(IValidator<CashCheckDTO> validator, ICashCheckRepository cashcheckrepository, ILogger logger)
    {
        _validator = validator;
        _cashcheckrepository = cashcheckrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashCheckCommand request, CancellationToken cancellationToken)
    {
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

        var cashcheck = await _cashcheckrepository.AddCashCheckForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.Cost, request.DTO.BankId, request.DTO.Branch, request.DTO.AccountNumber);

        if (cashcheck.Id != null)
        {
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = cashcheck.Id
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashcheck.Id);
        }

        else
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = cashcheck.exception.Message
            });
        return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", cashcheck.exception.Message);
    }
}