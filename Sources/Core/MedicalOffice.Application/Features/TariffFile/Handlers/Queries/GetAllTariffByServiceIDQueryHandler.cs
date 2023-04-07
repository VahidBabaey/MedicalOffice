using AutoMapper;
using FluentValidation;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.Common.IDtos;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Application.Models.Logger;
using MedicalOffice.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Features.TariffFile.Handlers.Queries
{
    public class GetAllTariffByServiceIDQueryHandler : IRequestHandler<GetAllTariffByServiceIDQuery, BaseResponse>
    {
        private readonly IValidator<ServiceIdDTO> _validator;
        private readonly IServiceTariffRepository _tariffrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllTariffByServiceIDQueryHandler(
            IValidator<ServiceIdDTO> validator,
            IServiceTariffRepository tariffrepository, 
            ILogger logger)
        {
            _validator = validator;
            _tariffrepository = tariffrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetAllTariffByServiceIDQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.ServiceId, cancellationToken);
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
                var tariffsOfService = await _tariffrepository.GetTariffsOfService(request.OfficeId, request.ServiceId.ServiceId);
                var tariffsofServicePagination = tariffsOfService.Skip(request.Dto.Skip).Take(request.Dto.Take);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = tariffsOfService.Count(), result = tariffsofServicePagination }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = tariffsOfService.Count(), result = tariffsofServicePagination });
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
