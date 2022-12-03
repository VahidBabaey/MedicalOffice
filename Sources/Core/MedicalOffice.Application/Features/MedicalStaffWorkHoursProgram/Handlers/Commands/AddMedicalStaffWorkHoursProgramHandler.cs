﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFileDTO.Validators;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Handlers.Commands
{

    public class AddMedicalStaffWorkHoursProgramHandler : IRequestHandler<AddMedicalStaffWorkHoursProgramCommand, BaseResponse>
    {
        private readonly IMedicalStaffWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;
        MedicalStaffWorkHourProgram MedicalStaffworkhourprogram = null;

        public AddMedicalStaffWorkHoursProgramHandler(IMedicalStaffWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            MedicalStaffworkhourprogram = new MedicalStaffWorkHourProgram();
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddMedicalStaffWorkHoursProgramCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            AddMedicalStaffWorkHoursProgramValidator validator = new();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    var workHourProgram = new List<Guid>();
                    foreach (var item in request.DTO.MedicalStaffWorkHours)
                    {
                        MedicalStaffworkhourprogram.MedicalStaffId = request.DTO.MedicalStaffId;
                        MedicalStaffworkhourprogram.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                        MedicalStaffworkhourprogram.WeekDay = item.WeekDay;
                        MedicalStaffworkhourprogram.MorningStart = item.MorningStart.ToTimeSpan();
                        MedicalStaffworkhourprogram.MorningEnd = item.MorningEnd.ToTimeSpan();
                        MedicalStaffworkhourprogram.EveningStart = item.EveningStart.ToTimeSpan();
                        MedicalStaffworkhourprogram.EveningEnd = item.EveningEnd.ToTimeSpan();

                        var MedicalStaffprogram = await _repository.Add(MedicalStaffworkhourprogram);
                        workHourProgram.Add(MedicalStaffprogram.Id);
                    }

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data = workHourProgram;

                    log.Type = LogType.Success;
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
