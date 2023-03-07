using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.SectionDTO;

public class AddSectionDTO
{
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool IsActive { get; set; }
}