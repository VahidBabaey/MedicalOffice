using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.OfficeFeature.Handlers.Commands
{
    public class AddOfficeCommandHandler : IRequestHandler<AddOfficeCommand, BaseResponse>
    {
        private readonly IValidator<OfficeDTO> _validator;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;
        public AddOfficeCommandHandler(
            IValidator<OfficeDTO> validator,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IOfficeRepository officeRepository,
            ILogger logger,
            IMapper mapper
            )
        {
            _validator = validator;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _officeRepository = officeRepository;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
        {
            var responseBuilder = new ResponseBuilder();

            var validationResult = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = validationResult.Errors.Select(error => error.ErrorMessage).ToArray()
                });

                return responseBuilder.Faild(HttpStatusCode.BadRequest,
                    $"{_requestTitle} failed",
                    validationResult.Errors.Select(error => error.ErrorMessage).ToArray());
            }

            var existingOffice = _officeRepository.GetAll().Result.Any(x => x.TelePhoneNumber == request.Dto.TelePhoneNumber);

            if (existingOffice)
            {
                var error = "An office with this phone number exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });

                return responseBuilder.Success(HttpStatusCode.Conflict,
                    $"{_requestTitle} succeeded",
                    error);
            }

            try
            {
                var office = _mapper.Map<Office>(request.Dto);

                var newOffice = _officeRepository.Add(office);

                await _officeRepository.SoftDelete(newOffice.Result.Id);

                if (request.Roles.Any(x=>x.Equals(AdminRole.Name)))
                {
                    var userOfficeRoles = _userOfficeRoleRepository.Add(new UserOfficeRole
                    {
                        UserId = request.UserId,
                        RoleId = AdminRole.Id,
                        OfficeId = newOffice.Result.Id,
                    });
                }

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeeded",
                    AdditionalData = newOffice
                });

                return responseBuilder.Success(HttpStatusCode.OK,
                    $"{_requestTitle} succeeded",
                    newOffice.Result.Id);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });

                return responseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}
