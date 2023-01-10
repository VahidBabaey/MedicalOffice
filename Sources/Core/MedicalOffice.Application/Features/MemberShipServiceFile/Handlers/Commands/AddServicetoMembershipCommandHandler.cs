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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MemberShipServiceFile.Handlers.Commands
{
    public class AddServicetoMembershipCommandHandler : IRequestHandler<AddServicetoMembershipCommand, BaseResponse>
    {
        private readonly IValidator<MemberShipServiceDTO> _validator;
        private readonly IMemberShipServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddServicetoMembershipCommandHandler(IValidator<MemberShipServiceDTO> validator, IMemberShipServiceRepository repository, IMapper mapper, ILogger logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);

        }

        public async Task<BaseResponse> Handle(AddServicetoMembershipCommand request, CancellationToken cancellationToken)
        {

            BaseResponse response = new();

            Log log = new();

            var validationResult = await _validator.ValidateAsync(request.DTO, cancellationToken);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

                log.Type = LogType.Error;
            }
            else
            {
                try
                {

                    foreach (var srvid in request.DTO.ServiceId)
                    {
                        await _repository.InsertServiceToMemberShipAsync(request.DTO.Discount, srvid, request.DTO.MembershipId);
                    }

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";

                    log.Type = LogType.Success;
                }
                catch (Exception error)
                {
                    response.Success = false;
                    response.StatusDescription = $"{_requestTitle} failed";
                    response.Errors.Add(error.Message);

                    log.Type = LogType.Error;
                }
            }

            log.Header = response.StatusDescription;
            log.AdditionalData = response.Errors;

            await _logger.Log(log);

            return response;
        }
    }
}
