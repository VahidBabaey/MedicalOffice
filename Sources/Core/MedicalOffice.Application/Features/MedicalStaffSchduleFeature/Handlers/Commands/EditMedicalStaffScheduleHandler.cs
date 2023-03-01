using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System.Linq;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Commands
{

    public class EditMedicalStaffScheduleHandler : IRequestHandler<EditMedicalStaffScheduleCommand, BaseResponse>
    {
        private readonly IValidator<MedicalStaffScheduleDTO> _validator;
        private readonly IMedicalStaffScheduleRepository _medicalStaffScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditMedicalStaffScheduleHandler(
            IValidator<MedicalStaffScheduleDTO> validator,
            IMedicalStaffScheduleRepository repository,
            IMapper mapper,
            ILogger logger
            )
        {
            _validator = validator;
            _medicalStaffScheduleRepository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditMedicalStaffScheduleCommand request, CancellationToken cancellationToken)
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
            var existingSchedule = _medicalStaffScheduleRepository.GetMedicalStaffScheduleByStaffId(request.DTO.MedicalStaffId, request.OfficeId).Result.ToList();

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

            #region RemoveOldStaffSchedules
            var staffNewDays = request.DTO.MedicalStaffSchedule.Select(x => x.WeekDay).ToList();

            var removableDays = existingSchedule.Where(x => !staffNewDays.Contains(x.WeekDay)).ToList();

            if (removableDays.Count != 0)
            {
                foreach (var item in removableDays)
                {
                    item.IsDeleted = true;
                }
                await _medicalStaffScheduleRepository.UpdateRange(removableDays);
            }
            #endregion

            #region UpdateSchedules
            var medicalStaffSchedule = new List<MedicalStaffSchedule>();
            if (request.DTO.MedicalStaffSchedule.Count != 0)
            {
                foreach (var item in request.DTO.MedicalStaffSchedule)
                {
                    if (existingSchedule.Select(e => e.WeekDay).Contains(item.WeekDay))
                    {
                        var staffSchedule = existingSchedule.First(e => e.WeekDay == item.WeekDay);
                        staffSchedule.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                        staffSchedule.MorningStart = item.MorningStart;
                        staffSchedule.MorningEnd = item.MorningEnd;
                        staffSchedule.EveningStart = item.EveningStart;
                        staffSchedule.EveningEnd = item.EveningEnd;

                        medicalStaffSchedule.Add(staffSchedule);
                    }
                    else
                    {
                        var schedule = _mapper.Map<MedicalStaffSchedule>(item);
                        schedule.MedicalStaffId = request.DTO.MedicalStaffId;
                        schedule.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                        schedule.OfficeId = request.OfficeId;

                        medicalStaffSchedule.Add(schedule);
                    }
                }
            }

            await _medicalStaffScheduleRepository.UpdateRange(medicalStaffSchedule);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = medicalStaffSchedule.Select(x => x.Id)
            });

            return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", medicalStaffSchedule.Select(x => x.Id));
            #endregion
        }
    }
}
