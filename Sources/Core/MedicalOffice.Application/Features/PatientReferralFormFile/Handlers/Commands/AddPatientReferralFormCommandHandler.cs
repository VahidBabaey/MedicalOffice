using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.PatientReferralFormDTO.Validator;
using MedicalOffice.Application.Features.PatientReferralFormFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.PatientReferralFormFile.Handlers.Commands
{

    public class AddPatientReferralFormCommandHandler : IRequestHandler<AddPatientReferralFormCommand, BaseResponse>
    {
        private readonly IPatientReferralFormRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddPatientReferralFormCommandHandler(IPatientReferralFormRepository repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddPatientReferralFormCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();

            AddPatientReferralFormValidator validator = new();

            Log log = new();

            var validationResult = await validator.ValidateAsync(request.DTO, cancellationToken);

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
                    var patientreferralform = _mapper.Map<PatientReferralForm>(request.DTO);

                    patientreferralform = await _repository.Add(patientreferralform);

                    response.Success = true;
                    response.StatusDescription = $"{_requestTitle} succeded";
                    response.Data = (new { Id = patientreferralform.Id });

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
