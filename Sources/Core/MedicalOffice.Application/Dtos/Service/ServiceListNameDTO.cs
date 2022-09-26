namespace MedicalOffice.Application.Dtos.Service;

public class ServiceListNameDTO
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }

    /// <summary>
    /// نام خدمت
    /// </summary>
    public string Name { get; set; } = string.Empty;


}
