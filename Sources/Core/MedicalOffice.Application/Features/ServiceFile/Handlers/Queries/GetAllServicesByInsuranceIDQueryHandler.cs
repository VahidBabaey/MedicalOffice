using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries;

public class GetAllServicesByInsuranceIDQueryHandler : IRequestHandler<GetAllServicesByInsuranceIDQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IInsuranceRepository _insurancerepository;
    private readonly IServiceRepository _servicerepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllServicesByInsuranceIDQueryHandler(IInsuranceRepository insurancerepository, IOfficeRepository officeRepository, IServiceRepository servicerepository, IMapper mapper, ILogger logger)
    {
        _insurancerepository = insurancerepository;
        _officeRepository = officeRepository;
        _servicerepository = servicerepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllServicesByInsuranceIDQuery request, CancellationToken cancellationToken)
    {
        var validationInsuranceId = await _insurancerepository.CheckExistInsuranceId(request.OfficeId, request.InsuranceId);

        if (!validationInsuranceId)
        {
            var error = "InsuranceID isn't exist";
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
            var services = await _servicerepository.GetByInsuranceId(request.OfficeId, request.InsuranceId);
            var result = _mapper.Map<List<ServicesByInsuranceIdDTO>>(services);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = services.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = services.Count(), result = result });
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
