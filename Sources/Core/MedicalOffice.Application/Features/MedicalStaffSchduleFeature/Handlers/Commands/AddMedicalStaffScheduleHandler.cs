using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO.Validators;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Commands
{
    public class AddMedicalStaffScheduleHandler : IRequestHandler<AddMedicalStaffScheduleCommand, BaseResponse>
    {
        private readonly IValidator<MedicalStaffScheduleDTO> _validator;
        private readonly IMedicalStaffScheduleRepository _medicalStaffScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public AddMedicalStaffScheduleHandler(
            IValidator<MedicalStaffScheduleDTO> validator,
            IMedicalStaffScheduleRepository medicalStaffScheduleRepository,
            IMapper mapper,
            ILogger logger)
        {
            _validator = validator;
            _medicalStaffScheduleRepository = medicalStaffScheduleRepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffScheduleCommand request, CancellationToken cancellationToken)
        {
            #region CheckValidation
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }
            #endregion

            #region CheckScheduleExist
            var existingSchedule = _medicalStaffScheduleRepository.GetMedicalStaffScheduleByStaffId(request.DTO.MedicalStaffId,request.OfficeId).Result.ToList();

            if (existingSchedule.Count != 0)
            {
                var error = "This staff schedule is exist";

                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            #endregion

            #region AddNewWorkHours
            var newWorkHours = new List<MedicalStaffSchedule>();

            if (request.DTO.MedicalStaffSchedule.Count != 0)
            {
                var MedicalStaffSchedule = new List<MedicalStaffSchedule>();

                foreach (var item in request.DTO.MedicalStaffSchedule)
                {
                    var schedule = _mapper.Map<MedicalStaffSchedule>(item);
                    schedule.MedicalStaffId = request.DTO.MedicalStaffId;
                    schedule.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                    schedule.OfficeId = request.OfficeId;

                    MedicalStaffSchedule.Add(schedule);
                }
                newWorkHours = await _medicalStaffScheduleRepository.AddRange(MedicalStaffSchedule);
            }

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = newWorkHours.Select(x => x.Id)
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", newWorkHours.Select(x => x.Id));
            #endregion
        }
    }
}