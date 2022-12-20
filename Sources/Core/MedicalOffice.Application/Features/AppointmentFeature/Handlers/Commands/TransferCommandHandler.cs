using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Commands
{
    public class TransferCommandHandler : IRequestHandler<TransferCommand, BaseResponse>
    {
        private readonly IValidator<TransferAppointmentDTO> _validator;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMedicalStaffRepository _medicalStaffRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IDeviceRepository _deviceRepository;

        private readonly string _requestTitle;

        public TransferCommandHandler(
            IValidator<TransferAppointmentDTO> validator,
            ILogger logger,
            IMapper mapper,
            IAppointmentRepository appointmentRepository,
            IMedicalStaffRepository medicalStaffRepository,
            IServiceRepository serviceRepository,
            IDeviceRepository deviceRepository)
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

        public async Task<BaseResponse> Handle(TransferCommand request, CancellationToken cancellationToken)
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
            if (existingAppointment == null)
            {
                var error = "The appointment isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var newAppointment = _mapper.Map<Appointment>(existingAppointment);
            newAppointment = _mapper.Map<Appointment>(request.DTO);

            if (request.DTO.RoomId != null && request.DTO.DeviceId == null)
            {
                var roomHasDevice = _deviceRepository.GetDevicesByRoomId((Guid)request.DTO.RoomId).Result
                    .Contains(new Device { Id = existingAppointment.DeviceId });

                if (!roomHasDevice)
                {
                    newAppointment.DeviceId = default;
                }
            }

            //if (request.DTO.ServiceId != null)
            //{
            //    var serviceId = _serviceRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.ServiceId);
            //    if (serviceId == null)
            //    {
            //        var error = "Service id does not exist!";
            //        await _logger.Log(new Log
            //        {
            //            Type = LogType.Error,
            //            Header = $"{_requestTitle} failed",
            //            AdditionalData = error
            //        });

            //        return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //    }
            //}
            //else
            //{
            //    newAppointment.ServiceId = existingAppointment.ServiceId;
            //}

            //if (request.DTO.MedicalStaffId != null)
            //{
            //    var medicalStaff = _medicalStaffRepository.GetAll().Result.FirstOrDefault(x => x.Id == request.DTO.MedicalStaffId);
            //    if (medicalStaff == null)
            //    {
            //        var error = "MedicalStaffId does not exist!";
            //        await _logger.Log(new Log
            //        {
            //            Type = LogType.Error,
            //            Header = $"{_requestTitle} failed",
            //            AdditionalData = error
            //        });

            //        return responseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            //    }
            //}
            //else
            //{
            //    newAppointment.MedicalStaffId = existingAppointment.MedicalStaffId;
            //}

            //if (request.DTO.DeviceId != null)
            //{
            //    var device = _deviceRepository.GetById((Guid)request.DTO.DeviceId);

            //    if (device == null)
            //    {
            //        throw new ArgumentNullException();
            //    }
            //}
            //else
            //{
            //    newAppointment.DeviceId = existingAppointment.DeviceId;
            //}

            //if (request.DTO.StartTime == null)
            //{
            //    newAppointment.StartTime = existingAppointment.StartTime;
            //}

            //if (request.DTO.EndTime == null)
            //{
            //    newAppointment.EndTime = existingAppointment.EndTime;
            //}

            var staffExistingAppointments = _appointmentRepository.GetByDateAndStaff(newAppointment.Date, medicalStaffId: newAppointment.MedicalStaffId).Result;
            var invalidAppointment = staffExistingAppointments.FirstOrDefault(x => !isValidTime(x, newAppointment.StartTime, newAppointment.EndTime));

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

            var deviceExistingAppointments = _appointmentRepository.GetByDateAndDevice(
                request.DTO.Date,
                deviceId: newAppointment.DeviceId,
                roomId: newAppointment.RoomId).Result;

            invalidAppointment = deviceExistingAppointments.FirstOrDefault(x => !isValidTime(x, newAppointment.StartTime, newAppointment.EndTime));

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

            await _appointmentRepository.Update(newAppointment);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = newAppointment.Id
            });
            return responseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", newAppointment.Id);
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
