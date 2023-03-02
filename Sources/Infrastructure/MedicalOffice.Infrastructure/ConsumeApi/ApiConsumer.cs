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
    public class ApiConsumer : IFetchData
    {
        private readonly ApiConsumerSettings _apiConsumersetting;
        private readonly IHttpContextAccessor _context;

        public ApiConsumer(IOptions<ApiConsumerSettings> apiConsumersetting, IHttpContextAccessor context)
        {
            _context = context;
            _apiConsumersetting = apiConsumersetting.Value;
        }
        public async Task<RestResponse> GetResponse(string path)
        {
            var queryString = string.Empty;
            if (_context.HttpContext != null)
            {
                var inputs = QueryHelpers.ParseQuery(_context.HttpContext.Request.QueryString.Value)
                             .ToDictionary(x => x.Key, x => x.Value);

                foreach (var item in inputs)
                {
                    queryString = string.Concat($"?{item.Key}={item.Value}");
                }
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
            services.AddTransient<IFetchData, ApiConsumer>();

            return services;
        }
    }
}