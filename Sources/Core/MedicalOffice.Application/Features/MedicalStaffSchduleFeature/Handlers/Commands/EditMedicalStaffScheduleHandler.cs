using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MedicalStaffScheduleFeature.Handlers.Commands
{

    public class EditMedicalStaffScheduleHandler : IRequestHandler<EditMedicalStaffScheduleCommand, BaseResponse>
    {
        private readonly IMedicalStaffScheduleRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditMedicalStaffScheduleHandler(IMedicalStaffScheduleRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditMedicalStaffScheduleCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            Log log = new();

            try
            {
                foreach (var item in request.DTO.MedicalStaffSchedule)
                {
                    await _repository.UpdateMedicalStaffsSchedule(
                        request.DTO.MedicalStaffId, 
                        (int)item.WeekDay, 
                        request.DTO);
                }

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";

                log.Type = LogType.Success;
            }
            catch (Exception error)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors.Add(error.Message);

                log.Type = LogType.Error;
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }

}
