using AutoMapper;
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

public class AddCashPosCommandHandler : IRequestHandler<AddCashPosCommand, BaseResponse>
{
    private readonly IValidator<CashPosDTO> _validator;
    private readonly ICashPosRepository _cashposrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public AddCashPosCommandHandler(IValidator<CashPosDTO> validator, ICashPosRepository cashposrepository, ILogger logger)
    {
        _validator = validator;
        _cashposrepository = cashposrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(AddCashPosCommand request, CancellationToken cancellationToken)
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
        else
        {
            try
            {
                var cashpos = await _cashposrepository.AddCashPosForAnyReceptionDetail(request.OfficeId, request.DTO.ReceptionId, request.DTO.Cost, request.DTO.BankId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = cashpos
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cashpos);
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

