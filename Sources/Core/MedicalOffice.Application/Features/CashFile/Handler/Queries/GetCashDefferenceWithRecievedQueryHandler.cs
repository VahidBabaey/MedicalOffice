using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Handler.Queries;

public class GetCashDefferenceWithRecievedQueryHandler : IRequestHandler<GetCashDefferenceWithRecievedQuery, CashDifferenceWithRecieved>
{
    private readonly ICashRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetCashDefferenceWithRecievedQueryHandler(ICashRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<CashDifferenceWithRecieved> Handle(GetCashDefferenceWithRecievedQuery request, CancellationToken cancellationToken)
    {
        CashDifferenceWithRecieved result = new();

        Log log = new();

        try
        {
            var cashdiffernece = await _repository.GetCashDifferenceWithRecieved(request.ReceptionId);

            result.CostDifference = cashdiffernece;

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.AdditionalData = error.Message;
            log.Type = LogType.Error;
        }

        await _logger.Log(log);

        return result;
    }

}
