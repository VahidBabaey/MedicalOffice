using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries;

public class GetServicesBySearchQueryHandler : IRequestHandler<GetServiceBySearchQuery, BaseResponse>
{
    private readonly ISectionRepository _sectionrepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IServiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetServicesBySearchQueryHandler(ISectionRepository sectionrepository, IOfficeRepository officeRepository, IServiceRepository repository, IMapper mapper, ILogger logger)
    {
        _sectionrepository = sectionrepository;
        _officeRepository = officeRepository;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetServiceBySearchQuery request, CancellationToken cancellationToken)
    {
        BaseResponse response = new();

        var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

        if (!validationOfficeId)
        {
            var error = $"OfficeID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = response.Errors
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        var validationSectionId = await _sectionrepository.CheckExistSectionId(request.OfficeId, request.SectionId);

        if (!validationSectionId)
        {
            var error = $"SectionID isn't exist";
            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} failed",
                AdditionalData = response.Errors
            });
            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
        }

        try
        {
            var services = await _repository.GetServiceBySearch(request.Name, request.SectionId);
            var result = _mapper.Map<List<ServiceListDTO>>(services.Where(p => p.OfficeId == request.OfficeId && p.SectionId == request.SectionId).Skip(request.Dto.Skip).Take(request.Dto.Take));

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
