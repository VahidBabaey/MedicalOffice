using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class DeleteCashPosCommandHandler : IRequestHandler<DeleteCashPosCommand, BaseResponse>
{
    private readonly ICashPosRepository _casgposrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteCashPosCommandHandler(ICashPosRepository casgposrepository, ILogger logger)
    {
        _casgposrepository = casgposrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteCashPosCommand request, CancellationToken cancellationToken)
    {

        bool iscashPosIdExist = await _casgposrepository.CheckCashPosId(request.CashPosId);

        if (!iscashPosIdExist)
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

        try
        {
            await _casgposrepository.DeleteCashPosForAnyReceptionDetail(request.CashPosId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded");
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


