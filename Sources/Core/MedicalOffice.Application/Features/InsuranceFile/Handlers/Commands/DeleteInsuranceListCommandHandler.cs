using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.SectionFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Commands;

public class DeleteInsuranceListCommandHandler : IRequestHandler<DeleteInsuranceListCommand, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IInsuranceRepository _insurancerepository;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public DeleteInsuranceListCommandHandler(IOfficeRepository officeRepository, IInsuranceRepository insurancerepository, ILogger logger)
    {
        _officeRepository = officeRepository;
        _insurancerepository = insurancerepository;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(DeleteInsuranceListCommand request, CancellationToken cancellationToken)
    {

        var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = "OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = error
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        foreach (var item in request.DTO.InsuranceId)
        {
            var validationSectionId = await _insurancerepository.CheckExistInsuranceId(request.OfficeId, item);

            if (!validationSectionId)
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
        }
        try
        {
            foreach (var item in request.DTO.InsuranceId)
            {
                await _insurancerepository.SoftDelete(item);
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
