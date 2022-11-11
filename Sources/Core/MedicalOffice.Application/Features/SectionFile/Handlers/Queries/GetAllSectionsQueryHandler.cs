using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Features.SectionFile.Requests.Queries;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.SectionFile.Handlers.Queries;

public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionQuery, List<SectionListDTO>>
{
    private readonly ISectionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllSectionsQueryHandler(ISectionRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<SectionListDTO>> Handle(GetAllSectionQuery request, CancellationToken cancellationToken)
    {
        List<SectionListDTO> result = new();

        Log log = new();

        try
        {
            var Section = await _repository.GetAllWithPaggination(request.Dto.Skip, request.Dto.Take);

            result = _mapper.Map<List<SectionListDTO>>(Section);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.AdditionalData=error.Message;
            log.Type = LogType.Error;
        }

        await _logger.Log(log);

        return result;
    }

}
