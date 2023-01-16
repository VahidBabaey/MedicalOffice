using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries;

public class GetServicesBySearchQueryHandler : IRequestHandler<GetServiceBySearchQuery, List<ServiceListDTO>>
{
    private readonly IServiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetServicesBySearchQueryHandler(IServiceRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<ServiceListDTO>> Handle(GetServiceBySearchQuery request, CancellationToken cancellationToken)
    {
        List<ServiceListDTO> result = new();

        Log log = new();

        try
        {
            var services = await _repository.GetServiceBySearch(request.Name);
            
            result = _mapper.Map<List<ServiceListDTO>>(services.Where(p => p.OfficeId == request.OfficeId && p.SectionId == request.SectionId));

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
