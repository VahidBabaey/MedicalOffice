namespace MedicalOffice.Application.Dtos.Reception;

public class ReceptionServiceDto
{
    public string ServiceName { get; set; } = string.Empty;
    public Guid ServiceId { get; set; }
    public float ServiceCount { get; set; }
    public Guid InsuranceId { get; set; }
    public Guid AdditionalInsuranceId { get; set; }
    public Guid[]? Users { get; set; }
    public long Received { get; set; }
    public Guid DiscountTypeId { get; set; }
    public long DiscountAmount { get; set; }
}
