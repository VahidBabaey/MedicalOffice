using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common.IValidators
{
    public class IReceptionIdValidator : AbstractValidator<IReceptionIdDTO>
    {
        private readonly IReceptionRepository _receptionRepository;
        private readonly IContextResolver _officeResolver;

        public IReceptionIdValidator(IReceptionRepository receptionRepository, IContextResolver officeResolver)
        {
            _officeResolver = officeResolver;
            _receptionRepository = receptionRepository;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.ReceptionId)
                .NotEmpty()
                .MustAsync(async (receptionId, token) =>
                {
                    var leaveTypeExists = await _receptionRepository.CheckExistReceptionId(officeId, receptionId);
                    if (leaveTypeExists == true)
                    {
                        return true;
                    }
                    return false;
                })
                .WithMessage("{PropertyName} isn't exist");
        }
    }
}
