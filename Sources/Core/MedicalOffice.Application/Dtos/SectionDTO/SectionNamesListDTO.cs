using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.SectionDTO;

public class SectionNamesListDTO: BaseDto<Guid>
{
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
