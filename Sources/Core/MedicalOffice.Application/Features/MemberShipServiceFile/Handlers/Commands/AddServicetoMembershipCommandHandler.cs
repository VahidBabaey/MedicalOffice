using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO;
using MedicalOffice.Application.Dtos.MemberShipServiceDTO.Validators;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Features.MemberShipServiceFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Commands
{
    public class AddServicetoMembershipCommandHandler : IRequestHandler<AddServicetoMembershipCommand, BaseResponse>
    {
        private readonly IValidator<MemberShipServiceDTO> _validator;
        private readonly IMemberShipServiceRepository _repository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddServicetoMembershipCommandHandler(IValidator<MemberShipServiceDTO> validator, IServiceRepository serviceRepository, IOfficeRepository officeRepository, IMemberShipServiceRepository repository, IMapper mapper, ILogger logger)
        {
            _serviceRepository = serviceRepository;
            _officeRepository = officeRepository;
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);

        }

        public async Task<BaseResponse> Handle(AddServicetoMembershipCommand request, CancellationToken cancellationToken)
        {

            BaseResponse response = new();

            var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = $"OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = response.Errors
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.Select(error => error.ErrorMessage).ToArray();
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = response.Errors
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }
            else
            {
                try
                {
                    var membershipservice = await _repository.InsertServiceToMemberShipAsync(request.OfficeId, request.DTO.Discount.ToString(), request.DTO.ServiceId, request.DTO.MembershipId);

                    await _logger.Log(new Log
                    {
                        Type = LogType.Success,
                        Header = $"{_requestTitle} succeded",
                        AdditionalData = membershipservice
                    });
                    return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", membershipservice);
                }
                catch (Exception error)
                {
                    await _logger.Log(new Log
                    {
                        Type = LogType.Error,
                        Header = $"{_requestTitle} failed",
                        AdditionalData = error.Message
                    });
                    return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error.Message);
                }
            }
        }
    }
}
