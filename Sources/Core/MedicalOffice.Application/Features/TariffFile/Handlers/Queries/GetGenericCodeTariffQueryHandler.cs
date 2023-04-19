using AutoMapper;
using AutoMapper.Internal;
using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Contracts.Persistence;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Dtos.Tariff;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Features.TariffFile.Requests.Queries;
using MedicalOffice.Application.Models.ApiConsumerModels;
using MedicalOffice.Application.Responses;
using MedicalOffice.Domain.Enums;
using Microsoft.AspNetCore.JsonPatch.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
#nullable disable
namespace MedicalOffice.Application.Features.TariffFile.Handlers.Queries
{
    public class GetGenericCodeTariffQueryHandler : IRequestHandler<GetGenericCodeTariffQuery, BaseResponse>
    {
        private readonly ApiConsumerSettings _apiConsumersetting;
        private readonly IContextResolver _QueryStringResolver;
        private readonly IApiConsumer _apiConsumer;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly string _requestTitle;

        public GetGenericCodeTariffQueryHandler(
            IOptions<ApiConsumerSettings> apiConsumersetting,
            IApiConsumer apiConsumer,
            IContextResolver officeResolver,
            IInsuranceRepository insuranceRepository)
        {
            _insuranceRepository = insuranceRepository;
            _apiConsumersetting = apiConsumersetting.Value;
            _apiConsumer = apiConsumer;
            _QueryStringResolver = officeResolver;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }

        public async Task<BaseResponse> Handle(GetGenericCodeTariffQuery request, CancellationToken cancellationToken)
        {
            var input = new List<ExternalApiInput>();
            input.Add(new ExternalApiInput
            {
                Key = nameof(request.DTO.GenericCode).ToLower(),
                Value = request.DTO.GenericCode
            });

            var response = await _apiConsumer.GetResponse(_apiConsumersetting.ServiceTariffsPath, input);

            if (!response.IsSuccessStatusCode)
            {
                return ResponseBuilder.Faild(HttpStatusCode.BadGateway, $"{_requestTitle} failed", response.ErrorMessage);
            }

            var tariff = await _insuranceRepository.GetTariffTypeByInsuranceId(request.DTO.InsuranceId, request.OfficeId);
            var tariffType = tariff.TariffType;

            var result = JsonConvert.DeserializeObject<ServiceTariffModel>(response.Content);

            foreach (var item in result.GetType().GetProperties())
            {
                if (Enum.GetName(tariffType) == item.Name)
                {
                    var res = new CalculateTariffsResDTO()
                    {
                        InsuranceTariff = (float)item.GetValue(result),

                        Difference = !request.DTO.CalculateDifference ? 0 :
                        request.DTO.Difference == null ?
                        result.Private - (float)item.GetValue(result) :
                        (float)request.DTO.Difference
                    };

                    res.Total = request.DTO.InsurancePersent == null ?
                        (float)item.GetValue(result) * (100 - tariff.InsurancePercent) / 100 + res.Difference :
                        (float)((float)item.GetValue(result) * (100 - request.DTO.InsurancePersent) / 100) + res.Difference;

                    return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", res);
                }
            }

            return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", new { });
        }
    }
}
