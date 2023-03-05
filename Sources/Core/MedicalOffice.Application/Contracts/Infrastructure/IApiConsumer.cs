using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using RestSharp;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IApiConsumer
    {
        Task<RestResponse> GetResponse(string path, List<ExternalApiInput> input);
    }
}