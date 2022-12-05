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
                #region UpdateExistingWorkHours
                var existingWorkHours = _repository.GetMedicalStaffWorkHourProgramByID(request.DTO.MedicalStaffId).Result.ToList();
                if (existingWorkHours != null)
                {
                    List<WeekDay> weekDays = existingWorkHours.Select(x => x.WeekDay)
                        .Intersect(request.DTO.MedicalStaffWorkHours.Select(x => x.WeekDay)).ToList();

                    if (weekDays != null)
                    {
                        foreach (var day in weekDays)
                        {
                            var dayWorkHour = existingWorkHours.SingleOrDefault(x => x.WeekDay == day);

                            var requestedWorkHour = request.DTO.MedicalStaffWorkHours.SingleOrDefault(x => x.WeekDay == day);

                            dayWorkHour.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                            dayWorkHour.MorningStart = requestedWorkHour.MorningStart;
                            dayWorkHour.MorningEnd = requestedWorkHour.MorningEnd;
                            dayWorkHour.EveningStart = requestedWorkHour.EveningStart;
                            dayWorkHour.EveningEnd = requestedWorkHour.EveningEnd;

                            await _repository.Update(dayWorkHour);

                            request.DTO.MedicalStaffWorkHours.Remove(requestedWorkHour);
                        }
                    }
                }
                #endregion

                #region AddNewWorkHours
                var newWorkHours = new List<Guid>();

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
                    newWorkHours = await _repository.AddRangle(medicalStaffworkhourprogram);
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