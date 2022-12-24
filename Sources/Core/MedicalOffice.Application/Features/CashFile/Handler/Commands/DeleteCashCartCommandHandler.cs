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

public class DeleteCashCartCommandHandler : IRequestHandler<DeleteCashCartCommand, BaseResponse>
{
    private readonly ICashCartRepository _repository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteCashCartCommandHandler(ICashCartRepository repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteCashCartCommand request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();
        Log log = new();

        try
        {
            await _repository.Delete(request.CashCartId);

            response.Success = true;
            response.StatusDescription = $"{_requestTitle} succeded";
            response.Data = (new { Id = request.CashCartId });

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


