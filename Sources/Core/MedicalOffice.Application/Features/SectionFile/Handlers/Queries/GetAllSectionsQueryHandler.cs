using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Queries;

public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly ISectionRepository _sectionrepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllSectionsQueryHandler(IOfficeRepository officeRepository, ISectionRepository sectionrepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _sectionrepository = sectionrepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var section = _sectionrepository.GetAll().Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false).OrderByDescending(x => x.CreatedDate);
            if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
            {
                section = request.Order == Order.NewRecords ? section : section.OrderBy(x => x.CreatedDate);
            }

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
