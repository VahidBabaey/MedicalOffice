using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.InsuranceDTO;
using MedicalOffice.Application.Dtos.SectionDTO;
using MedicalOffice.Application.Responses;

namespace MedicalOffice.Application.Features.SectionFile.Requests.Queries;

public class GetAdditionalInsuranceNamesQuery : IRequest<BaseResponse>
{
    public Guid OfficeId { get; set; }
}
