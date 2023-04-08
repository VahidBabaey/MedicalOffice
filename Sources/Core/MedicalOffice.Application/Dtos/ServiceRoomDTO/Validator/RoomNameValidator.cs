using FluentValidation;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.ServiceRoomDTO.Validator
{
    public class RoomNameValidator : AbstractValidator<INameDTO>
    {
        private readonly IRoomRepository _serviceRoomRepository;
        private readonly IContextResolver _officeResolver;

        public RoomNameValidator(IRoomRepository serviceRoomRepository, IContextResolver officeResolver)
        {
            _serviceRoomRepository = serviceRoomRepository;
            _officeResolver = officeResolver;

            var officeId = _officeResolver.GetOfficeId().Result;

            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("ورود نام اتاق ضروری است")
            .MustAsync(async (roomName, token) =>
            {
                return await _serviceRoomRepository.isNameUniqe(roomName, officeId);
            })
            //.WithMessage("{PropertyName} is not uniqe");
            .WithMessage("نام اتاق باید یکتا باشد");
        }
    }
}
