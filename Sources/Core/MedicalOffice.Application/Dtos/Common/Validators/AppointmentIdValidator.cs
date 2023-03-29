using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Common.Validators
{
    public class AppointmentIdValidator : AbstractValidator<IAppointmentIdDTO>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IRouteResolver _officeResolver;

        public AppointmentIdValidator(IAppointmentRepository appointmentRepository, IRouteResolver officeResolver)
        {
            _appointmentRepository = appointmentRepository;
            _officeResolver = officeResolver;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.AppointmentId)
                .NotEmpty()
                //.WithMessage("{PropertyName} is required")
                .WithMessage("شناسه نوبت ضروری است")
                .MustAsync(async (appointmentId, token) =>
                {
                    return await _appointmentRepository.checkAppointmentExist(appointmentId, officeId);

                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
