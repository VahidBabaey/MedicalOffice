using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Queries;

public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllSectionsQueryHandler(IOfficeRepository officeRepository, ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
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

        try
        {
            var Section = await _repository.GetAll();
            var result = _mapper.Map<List<SectionListDTO>>(Section.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false).Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = Section.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = Section.Count(), result = result });
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
