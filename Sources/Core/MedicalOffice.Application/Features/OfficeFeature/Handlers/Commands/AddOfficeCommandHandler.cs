using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Constants;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.OfficeDTO;
using MedicalOffice.Application.Features.OfficeFeature.Requests.Commands;
using MedicalOffice.Application.Models.Logger;
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
        private readonly IValidator<AddOfficeDto> _validator;
        private readonly IUserResolverService _userResolverService;
        private readonly IUserOfficeRoleRepository _userOfficeRoleRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly string _requestTitle;

        public AddOfficeCommandHandler(
            IValidator<AddOfficeDto> validator,
            IUserResolverService userResolverService,
            IUserOfficeRoleRepository userOfficeRoleRepository,
            IOfficeRepository officeRepository,
            ILogger logger,
            IMapper mapper
            )
        {
            _validator = validator;
            _userResolverService = userResolverService;
            _userOfficeRoleRepository = userOfficeRoleRepository;
            _officeRepository = officeRepository;
            _logger = logger;
            _mapper = mapper;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(AddOfficeCommand request, CancellationToken cancellationToken)
        {
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

            var userId = await _userResolverService.GetUserId();
            var roles = await _userResolverService.GetOfficeRoles();

            try
            {
                var office = _mapper.Map<Office>(request.DTO);

                var newOffice = _officeRepository.Add(office);

                if (roles.Any(x => x.RoleId == AdminRole.Id))
                {
                    var userOfficeRoles = _userOfficeRoleRepository.Add(new UserOfficeRole
                    {
                        UserId = Guid.Parse(userId),
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

                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeeded", newOffice.Result.Id);
            }
            catch (Exception error)
            {
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error.Message
                });

                return ResponseBuilder.Faild(HttpStatusCode.InternalServerError,
                    $"{_requestTitle} failed",
                    error.Message);
            }
        }
    }
}
