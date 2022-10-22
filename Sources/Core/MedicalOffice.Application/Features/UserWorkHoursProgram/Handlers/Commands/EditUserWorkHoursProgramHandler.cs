using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Features.UserWorkHoursProgram.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.UserWorkHoursProgram.Handlers.Commands
{

    public class EditUserWorkHoursProgramHandler : IRequestHandler<EditUserWorkHoursProgramCommand, BaseCommandResponse>
    {
        private readonly IUserWorkHourProgramRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditUserWorkHoursProgramHandler(IUserWorkHourProgramRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseCommandResponse> Handle(EditUserWorkHoursProgramCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            Log log = new();

            try
            {
                foreach (var item in request.DTO.StaffWorkHours)
                {

                await _repository.UpdateUsersWorkHoursProgram(request.DTO.UserId, (int)item.Day, request.DTO);

                }

                response.Success = true;
                response.Message = $"{_requestTitle} succeded";

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.Message = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.Message;
            log.Messages = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
