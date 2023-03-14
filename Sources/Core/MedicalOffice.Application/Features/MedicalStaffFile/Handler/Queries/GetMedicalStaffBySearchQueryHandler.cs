﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Queries
{
    public class GetMedicalStaffBySearchQueryHandler : IRequestHandler<GetMedicalStaffBySearchQuery, BaseResponse>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetMedicalStaffBySearchQueryHandler(IMedicalStaffRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetMedicalStaffBySearchQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var medicalStaffs = _repository.GetMedicalStaffBySearch(request.Name, request.OfficeId).Result.OrderByDescending(x => x.CreatedDate);
                if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
                {
                    medicalStaffs = request.Order == Order.NewRecords ? medicalStaffs : medicalStaffs.OrderBy(x => x.CreatedDate);
                };
                var result = _mapper.Map<List<MedicalStaffListDTO>>(medicalStaffs.Skip(request.Dto.Skip).Take(request.Dto.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = medicalStaffs.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = medicalStaffs.Count(), result = result });
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
}