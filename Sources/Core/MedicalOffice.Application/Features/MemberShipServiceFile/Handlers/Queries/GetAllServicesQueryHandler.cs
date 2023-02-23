using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Queries;
public class GetAllServicesQueryHandler : IRequestHandler<GetAllServicesQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IServiceRepository _servicerepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllServicesQueryHandler(IOfficeRepository officeRepository, IServiceRepository servicerepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _servicerepository = servicerepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
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

        try
        {
            var services =  _servicerepository.GetAll().Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false);
            var result = _mapper.Map<List<ServiceListDTO>>(services);

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
