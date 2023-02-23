using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.MembershipFile.Handlers.Queries;

public class GetMembershipBySearchQueryHandler : IRequestHandler<GetMembershipBySearchQuery, BaseResponse>
{
    private readonly IOfficeRepository _officeRepository;
    private readonly IMembershipRepository _membershiprepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetMembershipBySearchQueryHandler(IOfficeRepository officeRepository, IMembershipRepository membershiprepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _membershiprepository = membershiprepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetMembershipBySearchQuery request, CancellationToken cancellationToken)
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
            var memberShip = _membershiprepository.GetMembershipBySearch(request.Name).Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false);
            var result = _mapper.Map<List<MembershipListDTO>>(memberShip.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = memberShip.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = memberShip.Count(), result = result });
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
