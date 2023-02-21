using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.ShiftDTO;
using MedicalOffice.Application.Features.ShiftFile.Requests.Query;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.ShiftFile.Handlers.Query
{

    public class GetAllShiftsQueryHandler : IRequestHandler<GetAllShiftsQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IShiftRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllShiftsQueryHandler(IOfficeRepository officeRepository, IShiftRepository repository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllShiftsQuery request, CancellationToken cancellationToken)
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
                var shifts = await _repository.GetAll();
                var result = _mapper.Map<List<ShiftListDTO>>(shifts.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false).Skip(request.Dto.Skip).Take(request.Dto.Take));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = shifts.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = shifts.Count(), result = result });
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
