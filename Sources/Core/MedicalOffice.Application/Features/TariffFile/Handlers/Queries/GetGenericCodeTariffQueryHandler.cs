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
        private readonly string _requestTitle;
        private readonly IInsuranceRepository _insuranceRepository;
        private readonly IMapper _mapper;

        public GetGenericCodeTariffQueryHandler(
            IApiConsumer apiConsumer,
            IContextResolver officeResolver,
            IOptions<ApiConsumerSettings> apiConsumersetting,
            IInsuranceRepository insuranceRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _insuranceRepository = insuranceRepository;
            _apiConsumersetting = apiConsumersetting.Value;
            _apiConsumer = apiConsumer;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _QueryStringResolver = officeResolver;
        }
        public async Task<BaseResponse> Handle(GetGenericCodeTariffQuery request, CancellationToken cancellationToken)
        {
            var queryStrings = await _QueryStringResolver.GetAllQueryStrings();

            var input = new List<ExternalApiInput>();
            foreach (var item in queryStrings)
            {
                if (item.Key.ToLower() == nameof(request.GenericCode).ToLower())
                {
                    input.Add(new ExternalApiInput
                    {
                        Key = item.Key,
                        Value = item.Value
                    });
                }
            }

            var response = await _apiConsumer.GetResponse(_apiConsumersetting.ServiceTariffsPath, input);

            if (!response.IsSuccessStatusCode)
            {
                return ResponseBuilder.Faild(HttpStatusCode.BadGateway, $"{_requestTitle} failed", response.ErrorMessage);
            }

            var tariffType = await _insuranceRepository.GetTariffTypeByInsuranceId(request.InsuranceId, request.OfficeId);

            var result = JsonConvert.DeserializeObject<ServiceTariffDTO>(response.Content);

            foreach (var item in result.GetType().GetProperties())
            {
                if (Enum.GetName(tariffType) == item.Name)
                {
                    return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", item.GetValue(result));
                }
            }

            return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", new { });
        }
    }
}
