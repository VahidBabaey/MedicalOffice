﻿using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.UserWorkHoursProgramFileDTO.Validators;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Features.UserWorkHoursProgram.Handlers.Commands
{

    public class AddUserWorkHoursProgramHandler : IRequestHandler<AddUserWorkHoursProgramCommand, BaseResponse>
    {
        private readonly IUserWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;
        MedicalStaffWorkHourProgram Userworkhourprogram = null;

        public AddUserWorkHoursProgramHandler(IUserWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            Userworkhourprogram = new MedicalStaffWorkHourProgram();
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddUserWorkHoursProgramCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            AddUserWorkHoursProgramValidator validator = new();

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
                    foreach (var item in request.DTO.StaffWorkHours)
                    {
                        Userworkhourprogram.Id = new Guid();
                        Userworkhourprogram.UserId = request.DTO.UserId;
                        Userworkhourprogram.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                        Userworkhourprogram.WeekDay = item.Day;
                        Userworkhourprogram.MorningStart = item.MorningStart;
                        Userworkhourprogram.MorningEnd = item.MorningEnd;
                        Userworkhourprogram.EveningStart = item.EveningStart;
                        Userworkhourprogram.EveningEnd = item.EveningEnd;
                        var Userprogram = await _repository.Add(Userworkhourprogram);

                    }
                    
                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data = (new { Id = Userworkhourprogram.Id });

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
