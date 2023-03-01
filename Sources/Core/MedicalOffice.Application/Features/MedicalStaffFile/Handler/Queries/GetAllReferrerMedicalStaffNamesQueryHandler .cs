using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Queries
{

    public class GetAllReferrerMedicalStaffNamesQueryHandler : IRequestHandler<GetAllReferrerMedicalStaffsNamesQuery, BaseResponse>
    {
        private readonly IMedicalStaffRepository _medicalstaffrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public GetAllReferrerMedicalStaffNamesQueryHandler(IMedicalStaffRepository medicalstaffrepository, IMapper mapper, ILogger logger)
        {
            _medicalstaffrepository = medicalstaffrepository;
            _mapper = mapper;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllReferrerMedicalStaffsNamesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var medicalStaffs = await _medicalstaffrepository.GetAllReferrerMedicalStaffsName(request.OfficeId);
                var result = _mapper.Map<List<MedicalStaffNameReferrerListDTO>>(medicalStaffs);

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