using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries;

public class GetAllServicesBySectionIDQuery : IRequest<List<ServiceListDTO>>
{
    public Guid SectionId { get; set; }
    public Guid OfficeId { get; set; }
}
