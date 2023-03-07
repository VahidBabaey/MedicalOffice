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
using MedicalOffice.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace MedicalOffice.Application.Features.ShiftFile.Handlers.Query
{

    public class GetAllShiftsQueryHandler : IRequestHandler<GetAllShiftsQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IShiftRepository _shiftrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllShiftsQueryHandler(IOfficeRepository officeRepository, IShiftRepository shiftrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _shiftrepository = shiftrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllShiftsQuery request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

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
                var shifts = _shiftrepository.GetAll().Result.Where(p => p.OfficeId == request.OfficeId && p.IsDeleted == false).OrderByDescending(X =>X.CreatedDate);
                if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
                {
                    shifts = request.Order == Order.NewRecords ? shifts : shifts.OrderBy(x => x.CreatedDate);
                }

                var result = _mapper.Map<List<ShiftListDTO>>(shifts.Skip(request.Dto.Skip).Take(request.Dto.Take));

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
