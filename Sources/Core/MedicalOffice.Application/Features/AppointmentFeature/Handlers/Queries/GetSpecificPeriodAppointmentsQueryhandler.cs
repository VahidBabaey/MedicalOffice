using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Dtos.MedicalStaffScheduleDTO;
using MedicalOffice.Application.Dtos.ServiceDurationDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Handlers.Helper;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain;
using MedicalOffice.Domain.Entities;
using MedicalOffice.WebApi.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class GetSpecificPeriodAppointmentsQueryhandler : IRequestHandler<GetSpecificPeriodAppointmentsQuery, BaseResponse>
    {
        private readonly IValidator<GetSpecificPeriodAppointmentDTO> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMedicalStaffScheduleRepository _staffScheduleRepository;
        private readonly IServiceDurationRepositopry _serviceDurationRepository;
        private readonly IDeviceRepository _deviceRepository;

        private readonly string _requestTitle;

        public GetSpecificPeriodAppointmentsQueryhandler(
            IValidator<GetSpecificPeriodAppointmentDTO> validator,
            IMapper mapper,
            ILogger logger,
            IAppointmentRepository appointmentRepository,
            IServiceRepository serviceRepository,
            IMedicalStaffScheduleRepository staffScheduleRepository,
            IServiceDurationRepositopry serviceDurationRepository,
            IDeviceRepository deviceRepository)
        {
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
            _staffScheduleRepository = staffScheduleRepository;
            _serviceDurationRepository = serviceDurationRepository;

            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _deviceRepository = deviceRepository;
        }

        public async Task<BaseResponse> Handle(GetSpecificPeriodAppointmentsQuery request, CancellationToken cancellationToken)
        {
            

            #region ValidateRequest
            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }
            #endregion

            #region DeclareListOfPeriodAppointments
            var result = new List<GetSpecificPeriodAppointmentResponseDTO>();
            #endregion

            #region CheckRoomContainsDevice
            if (request.DTO.RoomId != null && request.DTO.DeviceId != null)
            {
                var roomHasDevice = _deviceRepository.GetDevicesByRoomId((Guid)request.DTO.RoomId).Result
                    .Contains(new Device { Id = (Guid)request.DTO.DeviceId });

                if (!roomHasDevice)
                {
                    var error = "Device isn't exist in this room";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });

                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }
            }
            #endregion

            #region ifJustServiceIdNotNull
            if (request.DTO.MedicalStaffId == null && request.DTO.DeviceId == null)
            {
                //TODO: It should go to validator
                var existingService = _serviceRepository.GetById(request.DTO.ServiceId).Result;
                if (existingService == null)
                {
                    var error = "Service isn't exist in this room";
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
                }

                var serviceDuration = _serviceDurationRepository.GetAllByServiceId(request.DTO.ServiceId).Result;

                var medicalStaffIds = serviceDuration.Select(d => d.MedicalStaffId).ToList();

                var allStaffAppointments = new List<GetSpecificPeriodAppointmentResponseDTO>();

                foreach (var staffId in medicalStaffIds)
                {
                    var service = _serviceDurationRepository
                        .GetByServiceAndStaffId(serviceId: request.DTO.ServiceId, medicalStaffId: staffId).Result;

                    var appointments = _appointmentRepository.GetByPeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, staffId).Result.ToList();

                    var staffSchedule = _staffScheduleRepository.GetMedicalStaffScheduleById(staffId).Result.ToList();

                    var eachStaffAppointments = FreeTimeGenerator.StaffPeriodAppointmetsCounts(service, appointments, staffSchedule, request.DTO.StartDate, request.DTO.EndDate);

                    allStaffAppointments.AddRange(eachStaffAppointments);
                }

                var dateAppointments = new List<GetSpecificPeriodAppointmentResponseDTO>();

                var appointmentGroups = allStaffAppointments.OrderBy(a => a.Date)
                    .GroupBy(x => x.Date);

                foreach (var group in appointmentGroups)
                {
                    var dateAppointment = new GetSpecificPeriodAppointmentResponseDTO();

                    dateAppointment.Date = group.Key;
                    foreach (var appointment in group)
                    {
                        dateAppointment.AllTimes = +appointment.AllTimes;
                        dateAppointment.FullTimes = +appointment.FullTimes;
                        dateAppointment.FreeTimes.AddRange(appointment.FreeTimes);
                    }

                    dateAppointments.Add(dateAppointment);

                }
                result.AddRange(dateAppointments);
            }
            #endregion

            #region IfMedicalStaffIdNotNull
            if (request.DTO.MedicalStaffId != null)
            {
                var service = _serviceDurationRepository
                    .GetByServiceAndStaffId(serviceId: request.DTO.ServiceId, medicalStaffId: request.DTO.MedicalStaffId).Result;

                if (service == null)
                {
                    var error = new ArgumentException("There is no service for this staff");
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error.Message,
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
                }

                var appointments = _appointmentRepository.GetByPeriodAndStaff(request.DTO.StartDate, request.DTO.EndDate, request.DTO.MedicalStaffId).Result.ToList();

                if (request.DTO.DeviceId != null)
                {
                    var deviceAppointments = new List<AppointmentDetailsDTO>();
                    deviceAppointments = _appointmentRepository
                     .GetByPeriodAndDeviceId(request.DTO.StartDate, request.DTO.EndDate, request.DTO.DeviceId).Result.ToList();

                    appointments.AddRange(deviceAppointments);
                }

                var staffSchedule = _staffScheduleRepository.GetMedicalStaffScheduleById(request.DTO.MedicalStaffId).Result.ToList();
                if (staffSchedule == null)
                {
                    var error = new ArgumentException("This staff doesn't have schedule!");
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error.Message,
                    });

                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
                }
                result = FreeTimeGenerator.StaffPeriodAppointmetsCounts(service, appointments, staffSchedule, request.DTO.StartDate, request.DTO.EndDate);
            }
            #endregion

            await _logger.Log(new Log
            {
                Type = LogType.Error,
                Header = $"{_requestTitle} succeeded",
                AdditionalData = result,
            });

            return ResponseBuilder.Success(HttpStatusCode.BadRequest, $"{_requestTitle} succeeded", result);
        }
    }
}
