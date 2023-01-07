using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Commands
{

    public class EditBasicInfoDetailCommandHandler : IRequestHandler<EditBasicInfoDetailCommand, BaseResponse>
    {
        private readonly IValidator<UpdateBasicInfoDetailDTO> _validator;
        private readonly IBasicInfoDetailRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public EditBasicInfoDetailCommandHandler(IValidator<UpdateBasicInfoDetailDTO> validator, IBasicInfoDetailRepository repository, IMapper mapper, ILogger logger)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(EditBasicInfoDetailCommand request, CancellationToken cancellationToken)
        {
            BaseResponse response = new();
            Log log = new();

            bool isBasicInfoDetailIdExist = await _repository.CheckExistBasicInfoDetailId(request.DTO.Id);
            bool isBasicInfoIdExist = await _repository.CheckExistBasicInfoId(request.DTO.OfficeId, request.DTO.BasicInfoId);

            if (!isBasicInfoDetailIdExist || !isBasicInfoIdExist)
            {
                List<string> errors = new List<string>();
                var error = $"لطفا یک مورد را انتخاب کنید.";
                response.Success = false;
                response.StatusDescription = $"{_requestTitle} failed";
                errors = new List<string> { error };
                response.Errors = errors;

                log.Type = LogType.Error;

                return response;
            }

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
                var basicinfodetail = _mapper.Map<BasicInfoDetail>(request.DTO);

                await _repository.Update(basicinfodetail);

                response.Success = true;
                response.StatusDescription = $"{_requestTitle} succeded";
                response.Data = (new { Id = basicinfodetail.Id });

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
