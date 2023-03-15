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

public class DeleteCashCommandHandler : IRequestHandler<DeleteCashCommand, BaseResponse>
{
    private readonly ICashPosRepository _cashposrepository;
    private readonly ICashCartRepository _cashcartrepository;
    private readonly ICashCheckRepository _cashcheckrepository;
    private readonly ICashMoneyRepository _cashmoneyrepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;
    bool iscashPosIdExist;
    bool iscashCartIdExist;
    bool iscashCheckIdExist;
    bool iscashMoneyIdExist;

    public DeleteCashCommandHandler(ICashMoneyRepository cashmoneyrepository, ICashCheckRepository casgcheckrepository, ICashCartRepository casgcartrepository, ICashPosRepository casgposrepository, ILogger logger)
    {
        _cashposrepository = casgposrepository;
        _cashcheckrepository = casgcheckrepository;
        _cashcartrepository = casgcartrepository;
        _cashmoneyrepository = cashmoneyrepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteCashCommand request, CancellationToken cancellationToken)
    {

        if (request.Cashtype == (Domain.Enums.Cashtype?)1)
        {
            iscashPosIdExist = await _cashposrepository.CheckCashPosId(request.CashTypeId);
        }
        if (request.Cashtype == (Domain.Enums.Cashtype?)2)
        {
            iscashCartIdExist = await _cashcartrepository.CheckCashCartId(request.CashTypeId);
        }
        if (request.Cashtype == (Domain.Enums.Cashtype?)3)
        {
            iscashCheckIdExist = await _cashcheckrepository.CheckCashCheckId(request.CashTypeId);
        }
        if (request.Cashtype == (Domain.Enums.Cashtype?)4)
        {
            iscashMoneyIdExist = await _cashmoneyrepository.CheckCashMoneyId(request.CashTypeId);
        }
        if (!iscashPosIdExist && !iscashCartIdExist && !iscashCheckIdExist && !iscashMoneyIdExist)
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
            if (request.Cashtype == (Domain.Enums.Cashtype?)1)
            {
                await _cashposrepository.DeleteCashPosForAnyReceptionDetail(request.CashTypeId);
            }
            else if (request.Cashtype == (Domain.Enums.Cashtype?)2)
            {
                await _cashcartrepository.DeleteCashCartForAnyReceptionDetail(request.CashTypeId);
            }
            else if (request.Cashtype == (Domain.Enums.Cashtype?)3)
            {
                await _cashcheckrepository.DeleteCashCheckForAnyReceptionDetail(request.CashTypeId);
            }
            else if (request.Cashtype == (Domain.Enums.Cashtype?)4)
            {
                await _cashmoneyrepository.DeleteCashMoneyForAnyReceptionDetail(request.CashTypeId);
            }

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


