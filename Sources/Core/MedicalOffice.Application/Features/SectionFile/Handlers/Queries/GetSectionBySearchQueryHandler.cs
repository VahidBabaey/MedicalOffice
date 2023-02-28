using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Common;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Queries;

public class GetSectionBySearchQueryHandler : IRequestHandler<GetSectionBySearchQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly ISectionRepository _sectionrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetSectionBySearchQueryHandler(IOfficeRepository officeRepository, ISectionRepository sectionrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _sectionrepository = sectionrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetSectionBySearchQuery request, CancellationToken cancellationToken)
    {

        var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

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
            var section = await _sectionrepository.GetSectionBySearch(request.Name, request.OfficeId);
            var result = _mapper.Map<List<SectionListDTO>>(section.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = section.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = section.Count(), result = result });
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
