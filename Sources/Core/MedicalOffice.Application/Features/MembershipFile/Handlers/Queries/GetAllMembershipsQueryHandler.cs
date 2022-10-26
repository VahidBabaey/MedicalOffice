using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Features.MembershipFile.Handlers.Queries;

public class GetAllMembershipsQueryHandler : IRequestHandler<GetAllMemberships, List<MembershipListDTO>>
{
    private readonly IMembershipRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllMembershipsQueryHandler(IMembershipRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<MembershipListDTO>> Handle(GetAllMemberships request, CancellationToken cancellationToken)
    {
        List<MembershipListDTO> result = new();

        Log log = new();

        try
        {
            var memberShip = await _repository.GetAllWithPaggination(request.DTO.Skip, request.DTO.Take);

            result = _mapper.Map<List<MembershipListDTO>>(memberShip);

            log.Header = $"{_requestTitle} succeded";
            log.Type = LogType.Success;
        }
        catch (Exception error)
        {
            log.Header = $"{_requestTitle} failed";
            log.Messages.Add(error.Message);
            log.Type = LogType.Error;
        }

        await _logger.Log(log);

        return result;
    }

}
