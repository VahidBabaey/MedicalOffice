using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.InsuranceDTO;

public class AdditionalInsuranceNamesDTO : BaseDto<Guid>
{
    /// <summary>
    /// نام بیمه : تامین - مسلح - غیره
    /// </summary>
    public string Name { get; set; } = string.Empty;
}