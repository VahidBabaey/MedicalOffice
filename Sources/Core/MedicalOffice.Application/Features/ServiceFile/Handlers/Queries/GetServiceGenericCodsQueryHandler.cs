using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.ApiConsumerModels;
using MedicalOffice.Application.Responses;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
#nullable disable

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries
{
    public class GetServiceGenericCodsQueryHandler : IRequestHandler<GetServiceGenericCodsQuery, BaseResponse>
    {
        private readonly ApiConsumerSettings _apiConsumersetting;
        private readonly IQueryStringResolver _QueryStringResolver;
        private readonly IApiConsumer _apiConsumer;
        private readonly string _requestTitle;

        public GetServiceGenericCodsQueryHandler(
            IApiConsumer apiConsumer, 
            IQueryStringResolver officeResolver,
            IOptions<ApiConsumerSettings> apiConsumersetting)
        {
            _apiConsumersetting = apiConsumersetting.Value;
            _apiConsumer = apiConsumer;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _QueryStringResolver = officeResolver;
        }
        public async Task<BaseResponse> Handle(GetServiceGenericCodsQuery request, CancellationToken cancellationToken)
        {
            var queryStrings = await _QueryStringResolver.GetAllQueryStrings();

            var input = new List<ExternalApiInput>();
            foreach (var item in queryStrings)
            {
                input.Add(new ExternalApiInput
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
            var result = new List<ServiceGenericCodeDTO>();

            var response = await _apiConsumer.GetResponse(_apiConsumersetting.ServiceGenericCodsPath, input);

            if (!response.IsSuccessStatusCode)
            {
                return ResponseBuilder.Faild(HttpStatusCode.BadGateway, $"{_requestTitle} failed", response.ErrorMessage);
            }

            if (response.Content != null)
            {
                result = JsonConvert.DeserializeObject<List<ServiceGenericCodeDTO>>(response.Content);
            }

            return ResponseBuilder.Success(response.StatusCode, $"{_requestTitle} succeeded", result);
        }
    }
}