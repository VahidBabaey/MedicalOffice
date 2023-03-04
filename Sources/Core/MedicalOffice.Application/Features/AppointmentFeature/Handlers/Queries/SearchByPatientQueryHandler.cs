using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO;
using MedicalOffice.Application.Features.AppointmentFeature.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.AppointmentFeature.Handlers.Queries
{
    public class SearchByPatientQueryHandler : IRequestHandler<SearchByPatientQuery, BaseResponse>
    {
        private readonly ILogger _logger;
        private readonly IAppointmentRepository _appointmentRepository;

        private readonly string _requestTitle;
        public SearchByPatientQueryHandler(
            ILogger logger,
            IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _logger = logger;
            _requestTitle = _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(SearchByPatientQuery request, CancellationToken cancellationToken)
        {
            
            var patientAppointments = await _appointmentRepository.searchPatientAappointments(request.Input, request.Date, request.OfficeId);

            await _logger.Log(new Log
            {
                Type = LogType.Success,
                Header = $"{_requestTitle} succeded",
                AdditionalData = patientAppointments
            });

            return ResponseBuilder.Success(HttpStatusCode.OK,
                $"{_requestTitle} succeeded",
                patientAppointments);
        }
    }
}
