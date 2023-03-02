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
        private readonly IFetchData _fetchData;
        private readonly string _requestTitle;

        public GetServiceGenericCodsQueryHandler(IFetchData fetchData)
        {
            _fetchData = fetchData;
            _requestTitle = GetType().Name.Replace("QueryHandler", string.Empty);
        }
        public async Task<BaseResponse> Handle(GetServiceGenericCodsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ServiceGenericCodeDTO>();

            var response = await _fetchData.GetResponse("/Service/generic-code");

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