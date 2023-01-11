using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO.Validators;
using MedicalOffice.Application.Features.CashFile.Request.Commands;
using MedicalOffice.Application.Features.InsuranceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.CashFile.Handlers.Commands;

public class DeleteCashPosCommandHandler : IRequestHandler<DeleteCashPosCommand, BaseResponse>
{
    private readonly ICashPosRepository _repository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteCashPosCommandHandler(ICashPosRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteCashPosCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();
        Log log = new();

        bool iscashPosIdExist = await _repository.CheckCashPosId(request.CashPosId);

        if (!iscashPosIdExist)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors.Add("ID isn't exist");

            log.Type = LogType.Error;
            return response;
        }

        try
        {
            await _repository.Delete(request.CashPosId);

            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = request.CashPosId });

            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            response.Success = false;
            response.StatusDescription = $"{_requestTitle} failed";
            response.Errors.Add(error.Message);

            log.Type = LogType.Error;
        }

        log.Header = response.StatusDescription;
        log.AdditionalData = response.Errors;

        await _logger.Log(log);

        return response;
    }
}


