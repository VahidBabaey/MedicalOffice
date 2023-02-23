using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Commands
{
    public class DeleteMedicalStaffScheduleHandler : IRequestHandler<DeleteMedicalStaffScheduleCommand, BaseResponse>
    {
        private readonly IMedicalStaffScheduleRepository _scheduleRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public DeleteMedicalStaffScheduleHandler(
            IMedicalStaffScheduleRepository staffScheduleRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IMapper mapper,
            ILogger logger)
        {
            _medicalStaffRepository = medicalStaffRepository;
            _scheduleRepository = staffScheduleRepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(DeleteMedicalStaffScheduleCommand request, CancellationToken cancellationToken)
        {
            #region CheckStaffExist
            var isStaffExist = await _medicalStaffRepository.CheckMedicalStaffExist(request.MedicalStaffId, request.OfficeId);
            if (!isStaffExist)
            {
                var error = "The Staff isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return ResponseBuilder.Success(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            #endregion

            #region CheckScheduleExist
            var existingSchedule = _scheduleRepository.GetMedicalStaffScheduleByStaffId(request.MedicalStaffId, request.OfficeId).Result.ToList();

            if (existingSchedule.Count == 0)
            {
                var error = "This staff schedule isn't exist";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            #endregion

            #region
            await _scheduleRepository.DeleteMedicalStaffSchedule(request.MedicalStaffId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = new { Id = request.MedicalStaffId }
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", new { Id = request.MedicalStaffId });
            #endregion
        }
    }
}
