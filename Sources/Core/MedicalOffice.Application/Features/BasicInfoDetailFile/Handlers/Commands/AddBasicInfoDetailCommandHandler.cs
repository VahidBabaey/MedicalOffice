using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO;
using MedicalOffice.Application.Dtos.BasicInfoDetailDTO.Validator;
using MedicalOffice.Application.Features.BasicInfoDetailFile.Requests.Commands;
using MedicalOffice.Application.Models;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.BasicInfoDetailFile.Handlers.Commands
{

    public class AddBasicInfoDetailCommandHandler : IRequestHandler<AddBasicInfoDetailCommand, BaseResponse>
    {
        private readonly IValidator<BasicInfoDetailDTO> _validator;
        private readonly IOfficeRepository _officeRepository;
        private readonly IBasicInfoDetailRepository _basicinfodetailrepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public AddBasicInfoDetailCommandHandler(IOfficeRepository officeRepository, IValidator<BasicInfoDetailDTO> validator, IBasicInfoDetailRepository basicinfodetailrepository, IMapper mapper, ILogger logger)
        {
            _officeRepository = officeRepository;
            _validator = validator;
            _basicinfodetailrepository = basicinfodetailrepository;
            _mapper = mapper;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("CommandHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(AddBasicInfoDetailCommand request, CancellationToken cancellationToken)
        {

            var validationOfficeId = await _officeRepository.CheckExistOfficeId(request.OfficeId);

            if (!validationOfficeId)
            {
                var error = "OfficeID isn't exist";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            var validationBasicInfoDetailName = await _basicinfodetailrepository.CheckExistBasicInfoDetailName(request.DTO.InfoDetailName, request.DTO.BasicInfoId);

            if (validationBasicInfoDetailName)
            {
                var error = "Name Must be Unique";
                await _logger.Log(new Log
                {
                    Type = LogType.Error,
                    Header = $"{_requestTitle} failed",
                    AdditionalData = error
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
                    AdditionalData = error
                });
                return ResponseBuilder.Faild(HttpStatusCode.BadRequest, $"{_requestTitle} failed", error);
            }

            try
            {
                var basicinfodetail = _mapper.Map<BasicInfoDetail>(request.DTO);

                basicinfodetail = await _basicinfodetailrepository.Add(basicinfodetail);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = basicinfodetail.Id
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", basicinfodetail.Id);
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
