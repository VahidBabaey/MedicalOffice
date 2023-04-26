using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Tariff;

public class TariffListIdDTO 
{
    /// <summary>
    /// آی دی تعرفه
    /// </summary>
    public Guid[] TariffId { get; set; }
}