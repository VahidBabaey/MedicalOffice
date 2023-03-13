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

public class ReturnCashCommandHandler : IRequestHandler<ReturnCashCommand, BaseResponse>
{
    private readonly ICashRepository _cashrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public ReturnCashCommandHandler(ICashRepository cashrepository, ILogger logger)
    {
        _cashrepository = cashrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(ReturnCashCommand request, CancellationToken cancellationToken)
    {

        var validationCashId = await _cashrepository.CheckExistCashId(request.OfficeId, request.CashId);

        if (!validationCashId)
        {
            var error = "ID isn't exist";
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
                var cash = await _cashrepository.ReturnCash(request.OfficeId, request.CashId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = cash
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", cash);
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

