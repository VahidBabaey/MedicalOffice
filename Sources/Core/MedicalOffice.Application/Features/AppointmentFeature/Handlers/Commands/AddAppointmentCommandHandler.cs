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
#nullable disable

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
        private readonly IDeviceRepository _deviceRepository;

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

            var serviceId = _serviceRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.ServiceId);
            if (serviceId == null)
            {
                var error = "Service id does not exist!";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var medicalStaff = _medicalStaffRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.MedicalStaffId);
            if (medicalStaff == null)
            {
                var error = "MedicalStaffId does not exist!";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(request.DTO.Date, medicalStaffId: request.DTO.MedicalStaffId).Result;
            var invalidAppointment = staffExistingAppointments.FirstOrDefault(x => !isValidTime(x, request.DTO.StartTime, request.DTO.EndTime));

            if (invalidAppointment != null)
            {
                var error = "Staff isn't free in requested time";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            if (request.DTO.DeviceId != null)
            {
                var device = _deviceRepository.GetById((Guid)request.DTO.DeviceId);

                if (device==null)
                {
                    throw new ArgumentNullException();
                }

                var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(
                    request.DTO.Date, 
                    deviceId: request.DTO.DeviceId, 
                    roomId: request.DTO.RoomId).Result;

                invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !isValidTime(x, request.DTO.StartTime, request.DTO.EndTime));

                if (invalidAppointment != null)
                {
                    var error = "Device isn't free in requested time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            if (request.DTO.DeviceId == null &&
                request.DTO.RoomId != null)
            {
                var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(request.DTO.Date, roomId: request.DTO.RoomId).Result;
                invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !isValidTime(x, request.DTO.StartTime, request.DTO.EndTime));

                if (invalidAppointment != null)
                {
                    var error = "Room isn't free in requested time";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }

            var appointment = _mapper.Map<Appointment>(request.DTO);
            var result = _appointmentRepository.Add(appointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = new { result.Id }
            });

            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { result.Id });
        }

        static bool isValidTime(AppointmentDetailsDTO time, string startTime, string endTime)
        {
            var serviceTime = (TimeOnly.Parse(endTime) - TimeOnly.Parse(startTime)).TotalMinutes;

            if (TimeOnly.Parse(time.StartTime) < TimeOnly.Parse(endTime) &&
                TimeOnly.Parse(startTime) < TimeOnly.Parse(time.EndTime))
                return false;

            if (TimeOnly.Parse(time.StartTime) == TimeOnly.Parse(startTime) ||
                TimeOnly.Parse(time.EndTime) == TimeOnly.Parse(endTime))
                return false;

            if (
                (TimeOnly.Parse(startTime) - TimeOnly.Parse(time.StartTime)).TotalMinutes < serviceTime ||
                (TimeOnly.Parse(endTime) - TimeOnly.Parse(time.EndTime)).TotalMinutes < serviceTime)
                return false;

            return true;
        }
    }
}
