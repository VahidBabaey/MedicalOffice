using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Queries;
public class GetAllServicesOfMemberShipQueryHandler : IRequestHandler<GetAllServicesOfMemberShipQuery, List<ServicesOfMemeberShipListDTO>>
{
    private readonly IMemberShipServiceRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllServicesOfMemberShipQueryHandler(IMemberShipServiceRepository repository, IMapper mapper, ILogger logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<List<ServicesOfMemeberShipListDTO>> Handle(GetAllServicesOfMemberShipQuery request, CancellationToken cancellationToken)
    {
        List<ServicesOfMemeberShipListDTO> result = new();

        Log log = new();

        try
        {
            var service = await _repository.GetAllServicesOfMemberShip(request.MemberShipId);

            result = _mapper.Map<List<ServicesOfMemeberShipListDTO>>(service);

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
