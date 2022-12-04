using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO.Validators;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using MedicalOffice.Domain.Enums;
using System.Net;

namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Handlers.Commands
{

    public class AddMedicalStaffWorkHoursProgramHandler : IRequestHandler<AddMedicalStaffWorkHoursProgramCommand, BaseResponse>
    {
        private readonly IValidator<MedicalStaffWorkHoursProgramDTO> _validator;
        private readonly IMedicalStaffWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private readonly string _requestTitle;

        public AddMedicalStaffWorkHoursProgramHandler(
            IValidator<MedicalStaffWorkHoursProgramDTO> validator,
            IMedicalStaffWorkHourProgramRepository repository,
            IMapper mapper,
            ILogger logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffWorkHoursProgramCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

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

            try
            {
                var existingMedicalStaffWorkHours = _repository.GetMedicalStaffWorkHourProgramByID(request.DTO.MedicalStaffId).Result.ToList();

                if (existingMedicalStaffWorkHours != null)
                {
                    List<WeekDay> weekDays = existingMedicalStaffWorkHours.Select(x => x.WeekDay)
                        .Intersect(request.DTO.MedicalStaffWorkHours.Select(x => x.WeekDay)).ToList();

                    if (weekDays != null)
                    {
                        foreach (var weekDay in weekDays)
                        {
                            var existingWeekDayWorkHour = existingMedicalStaffWorkHours.SingleOrDefault(x => x.WeekDay == weekDay);

                            var newWeekDayWorkHour = request.DTO.MedicalStaffWorkHours.SingleOrDefault(x => x.WeekDay == weekDay);

                            existingWeekDayWorkHour.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                            existingWeekDayWorkHour.MorningStart = newWeekDayWorkHour.MorningStart;
                            existingWeekDayWorkHour.MorningEnd = newWeekDayWorkHour.MorningEnd;
                            existingWeekDayWorkHour.EveningStart = newWeekDayWorkHour.EveningStart;
                            existingWeekDayWorkHour.EveningEnd = newWeekDayWorkHour.EveningEnd;

                            await _repository.Update(existingWeekDayWorkHour);

                            request.DTO.MedicalStaffWorkHours.Remove(newWeekDayWorkHour);
                        }
                    }
                }

                if (request.DTO.MedicalStaffWorkHours.Count != 0)
                {
                    var medicalStaffworkhourprogram = new List<MedicalStaffWorkHourProgram>();
                    foreach (var item in request.DTO.MedicalStaffWorkHours)
                    {
                        var workHourPrograms = _mapper.Map<MedicalStaffWorkHourProgram>(item);
                        workHourPrograms.MedicalStaffId = request.DTO.MedicalStaffId;
                        workHourPrograms.MaxAppointmentCount = request.DTO.MaxAppointmentCount;

                        medicalStaffworkhourprogram.Add(workHourPrograms);
                    }

                    await _repository.AddRangle(medicalStaffworkhourprogram);
                }

                var result = _repository.GetAll().Result.Select(x => x.MedicalStaffId = request.DTO.MedicalStaffId);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = result
                }); ;

                return responseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    result);
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