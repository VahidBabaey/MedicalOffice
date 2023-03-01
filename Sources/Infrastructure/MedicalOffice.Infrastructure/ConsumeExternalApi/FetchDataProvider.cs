using MedicalOffice.Application;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using MedicalOffice.Application.Models.CryptoSetting;
using MedicalOffice.WebApi.Crypto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Infrastructure.FetchData
{
    public class FetchDataProvider : IFetchData
    {
        private readonly FetchDataSettings _fetchDataSettings;

        public FetchDataProvider(IOptions<FetchDataSettings> baseDomainSettings)
        {
            _fetchDataSettings = baseDomainSettings.Value;
        }
        public async Task<List<ServiceGenericCodeDTO>> GetResponse(List<QueryStringInput> input)
        {
            try
            {
                //var url = _fetchDataSettings.BaseDomain;
                var url = "https://localhost:7123/api/Service/generic-code";
                foreach (var item in input)
                {
                    url = url + $"?{item.Key}={item.Value}";
                }

                var client = new RestClient(url);

                var request = new RestRequest();

                var response = new RestResponse();
                try
                {
                    response = client.Execute(request);

                }
                catch (Exception ex)
                {

                    throw ex;
                }

                var result = JsonConvert.DeserializeObject<List<ServiceGenericCodeDTO>>(response.Content);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddFetchData(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FetchDataSettings>(configuration.GetSection("FetchDataSettings"));
            services.AddTransient<IFetchData, FetchDataProvider>();

            return services;
        }
    }
}
