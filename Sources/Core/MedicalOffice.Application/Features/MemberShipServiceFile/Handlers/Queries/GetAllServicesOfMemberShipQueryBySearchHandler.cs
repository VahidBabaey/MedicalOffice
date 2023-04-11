﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Queries;
public class GetAllServicesOfMemberShipQueryBySearchHandler : IRequestHandler<GetAllServicesOfMemberShipQueryBySearch, BaseResponse>
{
    private readonly IMembershipRepository _membershiprepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IMemberShipServiceRepository _membershipservicerepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly string _requestTitle;

    public GetAllServicesOfMemberShipQueryBySearchHandler(IMembershipRepository membershiprepository, IOfficeRepository officeRepository, IMemberShipServiceRepository membershipservicerepository, IMapper mapper, ILogger logger)
    {
        _membershiprepository = membershiprepository;
        _officeRepository = officeRepository;
        _membershipservicerepository = membershipservicerepository;
        _mapper = mapper;
        _logger = logger;
        _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
    }

    public async Task<BaseResponse> Handle(GetAllServicesOfMemberShipQueryBySearch request, CancellationToken cancellationToken)
    {

        var validationMembershipId = await _membershiprepository.CheckExistMembershipId(request.OfficeId, request.MemberShipId);

        if (!validationMembershipId)
        {
            var error = "MembershipID isn't exist";
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
            var services = _membershipservicerepository.GetAllServicesOfMemberShipBySearch(request.OfficeId, request.MemberShipId, request.Name).Result.OrderByDescending(x => x.CreatedDate);
            if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
            {
                services = request.Order == Order.NewRecords ? services : services.OrderBy(x => x.CreatedDate);
            }

            var result = _mapper.Map<List<ServicesOfMemeberShipListDTO>>(services.Skip(request.Dto.Skip).Take(request.Dto.Take));

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { total = services.Count(), result = result }
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = services.Count(), result = result });
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

        static object GetCreatedDate(ServicesOfMemeberShipListDTO x)
        {
            return x.CreatedDate;
        }
    }

}
