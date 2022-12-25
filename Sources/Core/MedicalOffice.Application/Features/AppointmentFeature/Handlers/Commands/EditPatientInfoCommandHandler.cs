﻿using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class EditAppointmentPatientCommandHandler : IRequestHandler<EditAppointmentPatientCommand, BaseResponse>
    {
        private readonly IValidator<PatientInfoDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly string _requestTitle;

        public EditAppointmentPatientCommandHandler(IValidator<PatientInfoDTO> validator, ILogger logger, IMapper mapper, IAppointmentRepository appointmentRepository, IMedicalStaffRepository medicalStaffRepository, IServiceRepository serviceRepository, IDeviceRepository deviceRepository)
        {
            _validator = validator;
            _logger = logger;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _medicalStaffRepository = medicalStaffRepository;
            _serviceRepository = serviceRepository;
            _deviceRepository = deviceRepository;

            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(EditAppointmentPatientCommand request, CancellationToken cancellationToken)
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

            var existingAppointment = _appointmentRepository.GetById(request.DTO.AppointmentId).Result;
            //if (existingAppointment == null)
            //{
            //    var error = "The appointment isn't exist";
            //    await _logger.Log(new Log
            //    {
            //        Type = LogType.Error,
            //        Header = $"{_requestTitle} failed",
            //        AdditionalData = error
            //    });
            //    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //}

            existingAppointment = _mapper.Map<Appointment>(request.DTO);

            await _appointmentRepository.Update(existingAppointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = existingAppointment.Id
            });
            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", existingAppointment.Id);
        }
    }
}
