using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator
{
    public class RoomNameValidator : AbstractValidator<INameDTO>
    {
        private readonly IRoomRepository _serviceRoomRepository;
        private readonly IQueryStringResolver _officeResolver;

        public RoomNameValidator(IRoomRepository serviceRoomRepository, IQueryStringResolver officeResolver)
        {
            _serviceRoomRepository = serviceRoomRepository;
            _officeResolver = officeResolver;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(async (roomName, token) =>
            {
                return await _serviceRoomRepository.isNameUniqe(roomName, officeId);
            })
            .WithMessage("{PropertyName} is not uniqe");
        }
    }
}
