using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.CashDTO;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.CashFile.Request.Queries;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.CashFile.Handler.Queries;

public class GetTotalDebtofReceptionQueryHandler : IRequestHandler<GetTotalDebtofReceptionQuery, BaseResponse>
{
    private readonly ICashRepository _cashrepository;
    private readonly IReceptionRepository _receptionrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetTotalDebtofReceptionQueryHandler(IReceptionRepository receptionrepository, ICashRepository cashrepository, IMapper mapper, ILogger logger)
    {
        _cashrepository = cashrepository;
        _receptionrepository = receptionrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetTotalDebtofReceptionQuery request, CancellationToken cancellationToken)
    {

        try
        {
            var validationReceptionId = await _receptionrepository.CheckExistReceptionId(request.OfficeId, request.ReceptionId);

            if (!validationReceptionId)
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

            var totaldebt = await _cashrepository.GetTotalDebtofReception(request.OfficeId, request.ReceptionId, request.PatientId);


            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = 1, result = totaldebt }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = 1, result = totaldebt });
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
