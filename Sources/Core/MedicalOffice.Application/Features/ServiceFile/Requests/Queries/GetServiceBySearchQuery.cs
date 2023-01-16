using MediatR;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Application.Dtos.ServiceDTO;

namespace MedicalOffice.Application.Features.ServiceFile.Requests.Queries;

public class GetServiceBySearchQuery : IRequest<List<ServiceListDTO>>
{
    public Guid OfficeId { get; set; }
    public Guid SectionId { get; set; }
    public string Name { get; set; }
}
