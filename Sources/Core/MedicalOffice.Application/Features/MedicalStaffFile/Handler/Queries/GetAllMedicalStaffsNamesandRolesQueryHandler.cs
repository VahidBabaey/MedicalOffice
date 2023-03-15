using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffDTO;
using MedicalOffice.Application.Features.MedicalStaffFile.Request.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffFile.Handler.Queries
{
    public class GetAllMedicalStaffsNamesandRolesQueryHandler : IRequestHandler<GetAllMedicalStaffsNamesandRolesQuery, BaseResponse>
    {
        private readonly IMedicalStaffRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllMedicalStaffsNamesandRolesQueryHandler(IMedicalStaffRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllMedicalStaffsNamesandRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var MedicalStaffs = await _repository.GetAllMedicalStaffsNamesandRoles(request.OfficeId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = MedicalStaffs.Count(), result = MedicalStaffs }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = MedicalStaffs.Count(), result = MedicalStaffs });
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
