using MediatR;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Features.ServiceFile.Requests.Queries;
using MedicalOffice.Application.Models.ConsumableUrlsOutputs;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Handlers.Queries
{
    public class GetServiceGenericCodsQueryHandler : IRequestHandler<GetServiceGenericCodsQuery, BaseResponse>
    {
        private readonly IFetchData _fetchData;

        public GetServiceGenericCodsQueryHandler(IFetchData fetchData)
        {
            _fetchData = fetchData;
        }
        public Task<BaseResponse> Handle(GetServiceGenericCodsQuery request, CancellationToken cancellationToken)
        {
            var input = new List<QueryStringInput>();
            input.Add(new QueryStringInput
            {
                Key = "Name",
                Value = request.Name
            });
            var response = _fetchData.GetResponse(input);

            Console.WriteLine(response);

            return Task.FromResult(ResponseBuilder.Success(System.Net.HttpStatusCode.OK, "", response));
        }
    }
}
