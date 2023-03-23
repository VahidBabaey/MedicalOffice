using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ReceptionDTO;
using MedicalOffice.Application.Dtos.ReceptionDTO.Validators;
using MedicalOffice.Application.Dtos.SectionDTO.Validators;
using MedicalOffice.Application.Features.ReceptionFile.Requests.Commands;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace MedicalOffice.Application.Features.ReceptionFile.Handlers.Commands;

public class CalculateServiceTariffCommandHandler : IRequestHandler<CalculateServiceTariffCommand, BaseResponse>
{
    private readonly IReceptionRepository _receptionrepository;
    private readonly ICashRepository _repositoryCash;
    private readonly IReceptionDebtRepository _repositoryDebt;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public CalculateServiceTariffCommandHandler(IReceptionDebtRepository repositoryDebt, ICashRepository repositoryCash, IReceptionRepository receptionrepository, IMapper mapper, ILogger logger)
    {
        _receptionrepository = receptionrepository;
        _mapper = mapper;
        _logger = logger;
        _repositoryCash = repositoryCash;
        _repositoryDebt = repositoryDebt;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(CalculateServiceTariffCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var receptiondiscount = await _receptionrepository.CalculateServiceTariff(request.ServiceId, request.ServiceCount, request.InsuranceId, request.AdditionalInsuranceId, request.Discount, request.Tariff);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = receptiondiscount
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", receptiondiscount);
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
