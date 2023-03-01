using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using RestSharp;

namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface IFetchData
    {
        Task<List<ServiceGenericCodeDTO>> GetResponse(List<QueryStringInput> input);
    }
}