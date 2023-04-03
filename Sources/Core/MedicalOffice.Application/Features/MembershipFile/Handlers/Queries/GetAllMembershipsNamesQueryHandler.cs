using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MembershipFile.Requests.Queries;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Net;

namespace MedicalOffice.Application.Features.MembershipFile.Handlers.Queries;

public class GetAllMembershipsNamesQueryHandler : IRequestHandler<GetAllMembershipsNames, BaseResponse>
{
    private readonly IMembershipRepository _membershiprepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllMembershipsNamesQueryHandler(IOfficeRepository officeRepository, IMembershipRepository membershiprepository, IMapper mapper, ILogger logger)
    {
        _officeRepository = officeRepository;
        _membershiprepository = membershiprepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }
    public async Task<BaseResponse> Handle(GetAllMembershipsNames request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _membershiprepository.GetAllMembershipsNames(request.OfficeId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
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
