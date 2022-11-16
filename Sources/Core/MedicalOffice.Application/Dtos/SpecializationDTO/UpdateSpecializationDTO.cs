using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.SpecializationDTO;

public class UpdateSpecializationDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام تخصص
    /// </summary>
    public string Name { get; set; } = string.Empty;

}
