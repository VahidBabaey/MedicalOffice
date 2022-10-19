using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Common;

namespace MedicalOffice.Application.Dtos.Patient;

public class PatientSearchDto : ListDto
{
    public Guid OfficeId { get; set; }
    public Dictionary<string, object> Filters { get; set; } = new();
}