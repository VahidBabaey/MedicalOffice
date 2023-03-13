using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Net;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Queries
{

    public class GetAllBasicInfoDetailQueryHandler : IRequestHandler<GetAllBasicInfoDetailQuery, BaseResponse>
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IBasicInfoDetailRepository _basicinfodetailrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllBasicInfoDetailQueryHandler(IOfficeRepository officeRepository, IBasicInfoDetailRepository basicinfodetailrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _basicinfodetailrepository = basicinfodetailrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllBasicInfoDetailQuery request, CancellationToken cancellationToken)
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

            var validationBasicInfoId = await _basicinfodetailrepository.CheckExistBasicInfoId(request.OfficeId, request.BasicInfoId);

            if (!validationBasicInfoId)
            {
                var error = "BasicInfoId isn't exist";
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
                var basicInfoDetails = await _basicinfodetailrepository.GetByBasicInfoId(request.BasicInfoId);

                if (request.Order != null && Enum.IsDefined(typeof(Order), request.Order))
                {
                    basicInfoDetails = request.Order == Order.NewRecords ? basicInfoDetails : basicInfoDetails.OrderBy(x => x.CreatedDate).ToList();
                }

                var result = basicInfoDetails.Skip(request.DTO.Skip).Take(request.DTO.Take).Select(x => _mapper.Map<BasicInfoDetailListDTO>(x));

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = basicInfoDetails.Count(), result = result }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = basicInfoDetails.Count(), result = result });
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
