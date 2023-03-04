using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using MedicalOffice.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;
#nullable disable

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries
{
    public class GetServiceGenericCodsQueryHandler : IRequestHandler<GetServiceGenericCodsQuery, BaseResponse>
    {
        private readonly IQueryStringResolver _officeResolver;
        private readonly IApiConsumer _fetchData;
        private readonly string _requestTitle;

        public GetServiceGenericCodsQueryHandler(IApiConsumer fetchData, IQueryStringResolver officeResolver)
        {
            _fetchData = fetchData;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
            _officeResolver = officeResolver;
        }
        public async Task<BaseResponse> Handle(GetServiceGenericCodsQuery request, CancellationToken cancellationToken)
        {
            var queryStrings = await _officeResolver.GetAllQueryStrings();

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

            var response = await _fetchData.GetResponse("/Service/generic-code", input);

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