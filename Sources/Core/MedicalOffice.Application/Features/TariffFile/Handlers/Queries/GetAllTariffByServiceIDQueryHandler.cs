using AutoMapper;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.InsuranceDTO;
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
        private readonly IServiceRepository _serviceRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IServiceTariffRepository _tariffrepository;
        private readonly ILogger _logger;
        private readonly string _requestTitle;

        public GetAllTariffByServiceIDQueryHandler(IServiceRepository serviceRepository, IOfficeRepository officeRepository, IServiceTariffRepository tariffrepository, ILogger logger)
        {
            _serviceRepository = serviceRepository;
            _officeRepository = officeRepository;
            _tariffrepository = tariffrepository;
            _logger = logger;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetAllTariffByServiceIDQuery request, CancellationToken cancellationToken)
        {
            Log log = new();

            var validationOfficeId = await _officeRepository.IsOfficeExist(request.OfficeId);

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

            var validationServiceId = await _serviceRepository.CheckExistServiceId(request.OfficeId, request.ServiceId);

            if (!validationServiceId)
            {
                var error = $"ServiceID isn't exist";
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
                var tariffsofService = await _tariffrepository.GetTariffsofService(request.OfficeId, request.ServiceId);
                var tariffsofServicePagination = tariffsofService.Skip(request.Dto.Skip).Take(request.Dto.Take);

                await _logger.Log(new Log
                {
                    Type = LogType.Success,
                    Header = $"{_requestTitle} succeded",
                    AdditionalData = new { total = tariffsofService.Count(), result = tariffsofServicePagination }
                });
                return ResponseBuilder.Success(HttpStatusCode.OK, $"{_requestTitle} succeded", new { total = tariffsofService.Count(), result = tariffsofServicePagination });
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
