using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.CommonValidators;
using MedicalOffice.Application.Dtos.Common.Validators;

namespace MedicalOffice.Application.Dtos.ReceptionDTO.Validators;

public class AddReceptionValidator : AbstractValidator<ReceptionsDTO>
{
    private readonly IMedicalStaffRepository _medicalStaffRepository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IQueryStringResolver _officeResolver;
    public AddReceptionValidator(IMedicalStaffRepository medicalStaffRepository, IShiftRepository shiftRepository, IPatientRepository patientRepository, IQueryStringResolver officeResolver)
    {
        _medicalStaffRepository = medicalStaffRepository;
        _shiftRepository = shiftRepository;
        _patientRepository = patientRepository;
        _officeResolver = officeResolver;

        Include(new PatientIdValidator(_patientRepository, _officeResolver));
        Include(new ShiftIdValidator(_shiftRepository, _officeResolver));
        Include(new MedicalStaffValidator(_medicalStaffRepository, _officeResolver));
    }
}
