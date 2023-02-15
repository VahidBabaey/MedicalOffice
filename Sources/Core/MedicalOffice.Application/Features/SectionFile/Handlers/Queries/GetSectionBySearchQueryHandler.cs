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
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetSectionBySearchQueryHandler(ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetSectionBySearchQuery request, CancellationToken cancellationToken)
    {
        Log log = new();

        try
        {
            var Section = await _repository.GetSectionBySearch(request.Name, request.Dto.Take, request.Dto.Skip);
            var result = _mapper.Map<List<SectionListDTO>>(Section.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false));

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
            log.AdditionalData = result;
            await _logger.Log(log);

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = result.Count(), result = result });
        }

        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.AdditionalData = error.Message;
            log.Type = LogType.Error;
            await _logger.Log(log);

            return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
        }
    }

}
