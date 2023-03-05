using MedicalOffice.Application;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;

namespace MedicalOffice.Infrastructure.FetchData
{
    public class ApiConsumer : IApiConsumer
    {
        private readonly ApiConsumerSettings _apiConsumersetting;

        public ApiConsumer(
            IOptions<ApiConsumerSettings> apiConsumersetting)
        {
            _apiConsumersetting = apiConsumersetting.Value;
        }

        public async Task<RestResponse> GetResponse(string path, List<ExternalApiInput> input)
        {
            var queryString = string.Empty;

            foreach (var item in input)
            {
                queryString = string.Concat(queryString, $"?{item.Key}={item.Value}&");
            }

            var url = string.Concat(_apiConsumersetting.BaseDomain, path, queryString);

            var client = new RestClient(url);

            var request = new RestRequest();

            var response = await client.ExecuteAsync(request);

            return response;
        }
    }

    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddFetchData(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiConsumerSettings>(configuration.GetSection("ApiConsumerSettings"));
            services.AddTransient<IApiConsumer, ApiConsumer>();

            return services;
        }
    }
}