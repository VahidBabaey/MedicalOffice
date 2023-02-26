using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Queries
{

    public class GetAllMedicalStaffScheduleHandler : IRequestHandler<GetAllMedicalStaffScheduleQuery, BaseResponse>
    {
        private readonly IMedicalStaffScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffScheduleHandler(
            IMedicalStaffScheduleRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllMedicalStaffScheduleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var medicalStaffSchedules = await _repository.GetMedicalStaffScheduleByStaffId(request.MedicalStaffId, request.OfficeId);

                var result = _mapper.Map<List<MedicalStaffScheduleListDTO>>(medicalStaffSchedules.Skip(request.DTO.Skip).Take(request.DTO.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = new { total = medicalStaffSchedules.Count, result = result }
                });

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", new { total = medicalStaffSchedules.Count, result = result });
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return ResponseBuilder.Faild(HttpStatusCode.InternalServerError, $"{_requestTitle} failed", error.Message);
            }
        }
    }
}
