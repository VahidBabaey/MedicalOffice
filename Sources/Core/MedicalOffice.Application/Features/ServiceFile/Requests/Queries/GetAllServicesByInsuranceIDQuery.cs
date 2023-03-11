using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries;

public class GetAllServicesByInsuranceIDQuery : IRequest<BaseResponse>
{
    public Guid InsuranceId { get; set; }
    public Guid OfficeId { get; set; }
}
