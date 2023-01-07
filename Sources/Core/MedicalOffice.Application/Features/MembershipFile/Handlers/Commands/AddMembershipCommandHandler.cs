using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.MembershipDTO;
using MedicalOffice.Application.Dtos.MembershipDTO.Validators;
using MedicalOffice.Application.Features.MembershipFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.MembershipFile.Handlers.Commands
{

    public class AddMembershipCommandHandler : IRequestHandler<AddMembershipCommand, BaseResponse>
    {
        private readonly IValidator<MembershipDTO> _validator;
        private readonly IMembershipRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddMembershipCommandHandler(IValidator<MembershipDTO> validator, IMembershipRepository repository, IMapper mapper, ILogger logger)
        {
            _validator = validator;
            _repository = repository;   
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);

        }

        public async Task<BaseResponse> Handle(AddMembershipCommand request, CancellationToken cancellationToken)
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
                    var membership = _mapper.Map<Membership>(request.DTO);

                    membership = await _repository.Add(membership);

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data=(new { Id = membership.Id });
                    //if (request.DTO.ServiceIDs == null)
                    //{

                    //}
                    //else
                    //{
                    //    foreach (var srvid in request.DTO.ServiceIDs)
                    //    {                   
                    //    await _officeRepository.InsertMembershipIdofServiceAsync(membership.Discount, srvid, membership.Id);
                    //    }
                    //}
                    

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
