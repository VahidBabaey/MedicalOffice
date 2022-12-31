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
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public AddMedicalStaffScheduleHandler(
            IValidator<MedicalStaffScheduleDTO> validator,
            IMedicalStaffScheduleRepository medicalStaffScheduleRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IMapper mapper,
            ILogger logger)
        {
            _validator = validator;
            _medicalStaffScheduleRepository = medicalStaffScheduleRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffScheduleCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

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

                return responseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }
            #endregion

            try
            {
                var existingStaff = _medicalStaffRepository.GetById(request.DTO.MedicalStaffId).Result;
                if (existingStaff == null)
                {
                    //var error = new ArgumentException("medicalStaff isn't exist");
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} faild",
                        AdditionalData = new ArgumentException("medicalStaff isn't exist").Message
                    });

                    return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                        $"{_requestTitle} failed",
                        new ArgumentException("medicalStaff isn't exist").Message);
                }
                #region UpdateExistingWorkHours
                var existingSchedule = _medicalStaffScheduleRepository.GetMedicalStaffScheduleByID(request.DTO.MedicalStaffId).Result.ToList();

                if (existingSchedule.Count != 0)
                {
                    List<DayOfWeek> weekDays = existingSchedule.Select(x => x.WeekDay)
                        .Intersect(request.DTO.MedicalStaffSchedule.Select(x => x.WeekDay)).ToList();

                    if (weekDays != null)
                    {
                        foreach (var day in weekDays)
                        {
                            var daySchedule = existingSchedule.SingleOrDefault(x => x.WeekDay == day);

                            var requestedSchedule = request.DTO.MedicalStaffSchedule.SingleOrDefault(x => x.WeekDay == day);

                            daySchedule.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                            daySchedule.MorningStart = requestedSchedule.MorningStart;
                            daySchedule.MorningEnd = requestedSchedule.MorningEnd;
                            daySchedule.EveningStart = requestedSchedule.EveningStart;
                            daySchedule.EveningEnd = requestedSchedule.EveningEnd;

                            await _medicalStaffScheduleRepository.Update(daySchedule);

                            request.DTO.MedicalStaffSchedule.Remove(requestedSchedule);
                        }
                    }
                }
                #endregion

                #region AddNewWorkHours
                var newWorkHours = new List<Guid>();

                if (request.DTO.MedicalStaffSchedule.Count != 0)
                {
                    var MedicalStaffSchedule = new List<MedicalStaffSchedule>();

                    foreach (var item in request.DTO.MedicalStaffSchedule)
                    {
                        var schedule = _mapper.Map<MedicalStaffSchedule>(item);
                        schedule.MedicalStaffId = request.DTO.MedicalStaffId;
                        schedule.MaxAppointmentCount = request.DTO.MaxAppointmentCount;

                        MedicalStaffSchedule.Add(schedule);
                    }
                    newWorkHours = await _medicalStaffScheduleRepository.AddRangle(MedicalStaffSchedule);
                }
                #endregion

                #region FinalResponse
                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = newWorkHours
                }); ;

                return responseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    newWorkHours);

                #endregion
            }
            catch (Exception error)

            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} faild",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}