using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class AppointmentIdValidator : AbstractValidator<IAppointmentIdDTO>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IContextResolver _officeResolver;

        public AppointmentIdValidator(IAppointmentRepository appointmentRepository, IContextResolver officeResolver)
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
