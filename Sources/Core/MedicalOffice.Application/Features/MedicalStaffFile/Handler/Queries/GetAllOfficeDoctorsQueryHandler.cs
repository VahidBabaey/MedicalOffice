using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Queries
{
    public class GetAllOfficeDoctorsQueryHandler : IRequestHandler<GetAllOfficeDoctorsQuery, BaseResponse>
    {
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllOfficeDoctorsQueryHandler(
            IMedicalStaffRepository medicalStaffRepository,
            IMapper mapper,
            ILogger logger
            )
        {
            _medicalStaffRepository = medicalStaffRepository;
            _mapper = mapper;
            _logger = logger;

            _requestTitle = GetType().Name.Replace("QueryHandler",string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllOfficeDoctorsQuery request, CancellationToken cancellationToken)
        {
            var staffs = await _medicalStaffRepository.GetAllDoctorsAndExperts(request.OfficeId);
            var staffNamse = _mapper.Map<List<MedicalStaffNameListDTO>>(staffs);
            var result = new List<NameDTO>();

            foreach (var item in staffNamse)
            {
                result.Add(new NameDTO
                {
                    Id = item.Id,
                    Name = item.FirstName + " " + item.LastName
                });
            }
            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = result
            });
            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", result);
        }
    }
}
