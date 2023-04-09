using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries
{
    public class GetServiceGenericCodsQuery : IRequest<BaseResponse>
    {
        public string SearchString { get; set; }
    }
}