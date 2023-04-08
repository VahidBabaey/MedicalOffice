using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IValidators;
using MedicalOffice.Application.Dtos.Common.IValidators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class AddReceptionValidator : AbstractValidator<ReceptionsDTO>
{
    private readonly IMedicalStaffRepository _medicalStaffRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IContextResolver _officeResolver;
    public AddReceptionValidator(IMedicalStaffRepository medicalStaffRepository, IShiftRepository shiftRepository, IPatientRepository patientRepository, IContextResolver officeResolver)
    {
        _medicalStaffRepository = medicalStaffRepository;
        _shiftRepository = shiftRepository;
        _patientRepository = patientRepository;
        _officeResolver = officeResolver;

        Include(new IPatientIdValidator(_patientRepository, _officeResolver));
    }
}
