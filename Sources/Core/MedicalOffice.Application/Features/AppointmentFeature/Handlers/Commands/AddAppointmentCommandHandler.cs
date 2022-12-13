using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.Identity;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class AddAppointmentCommandHandler : IRequestHandler<AddAppointmentCommand, BaseResponse>
    {
        private readonly IValidator<AppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;

        private readonly string _requestTitle;

        public AddAppointmentCommandHandler(
            IValidator<AppointmentDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository
            )
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
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

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }


            #region checkValidTimeApi
            //get staff date time
            //bool validTime = _staffWorkHourRepository.checkTimeIsInStaffWorkHours();
            //if(false)
            //add warning to data

            //get service duration time
            //var serviceDuration = await _serviceDurationRepository.getByIdAndMedicalStaffId();
            //if(serviceDuration>EndTime-StartTime)
            //add warning to data
            #endregion

            bool isValidTime(AppointmentDetailsDTO x)
            {
                if (TimeOnly.Parse(x.StartTime) >= TimeOnly.Parse(request.DTO.StartTime) &&
                    TimeOnly.Parse(x.EndTime) <= TimeOnly.Parse(request.DTO.EndTime))
                    return false;

                if (TimeOnly.Parse(x.StartTime) == TimeOnly.Parse(request.DTO.StartTime) ||
                    TimeOnly.Parse(x.EndTime) == TimeOnly.Parse(request.DTO.EndTime))
                    return false;

                return true;
            }

            var validAppointment = _appointmentRepository.GetByDate(request.DTO.date).Result.FirstOrDefault(x => !isValidTime(x));
            if (validAppointment != null)
            {
                throw new Exception();
            }

            var medicalStaff = _medicalStaffRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.MedicalStaffId);
            if (medicalStaff == null)
            {
                throw new Exception();
            }

            var serviceId = _serviceRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.ServiceId);
            if (serviceId == null)
            {
                throw new Exception();
            }

            var appointment = _mapper.Map<Appointment>(request.DTO);
            var result = _appointmentRepository.Add(appointment);
            throw new NotImplementedException();
        }
    }
}
