using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffWorkHoursProgramFile.Validators;
using MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;


namespace MedicalOffice.Application.Features.MedicalStaffWorkHoursProgram.Handlers.Commands
{

    public class AddMedicalStaffWorkHoursProgramHandler : IRequestHandler<AddMedicalStaffWorkHoursProgramCommand, BaseCommandResponse>
    {
        private readonly IMedicalStaffWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;
        MedicalStaffWorkHourProgram medicalstaffworkhourprogram = null;

        public AddMedicalStaffWorkHoursProgramHandler(IMedicalStaffWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            medicalstaffworkhourprogram = new MedicalStaffWorkHourProgram();
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(AddMedicalStaffWorkHoursProgramCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            AddMedicalStaffWorkHoursProgramValidator validator = new();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {
                    foreach (var item in request.DTO.StaffWorkHours)
                    {
                        medicalstaffworkhourprogram.Id = new Guid();
                        medicalstaffworkhourprogram.MedicalStaffId = request.DTO.UserId;
                        medicalstaffworkhourprogram.MaxAppointmentCount = request.DTO.MaxAppointmentCount;
                        medicalstaffworkhourprogram.WeekDay = item.Day;
                        medicalstaffworkhourprogram.MorningStart = item.MorningStart;
                        medicalstaffworkhourprogram.MorningEnd = item.MorningEnd;
                        medicalstaffworkhourprogram.EveningStart = item.EveningStart;
                        medicalstaffworkhourprogram.EveningEnd = item.EveningEnd;
                        var medicalstaffprogram = await _repository.Add(medicalstaffworkhourprogram);

                    }
                    


                    response.Success = true;
                    response.Message = $"{_requestTitle} succeded";
                    response.Data.Add(new { Id = medicalstaffworkhourprogram.Id });

                    log.Type = LogType.Success;
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.Message = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
