using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// کاربران / دکتران پذیرش
/// </summary>
public class ReceptionUser : BaseDomainEntity<Guid>
{
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ReceptionDetail? ReceptionDetail { get; set; }
    /// <summary>
    /// آیدی جزئیات پذیرش
    /// </summary>
    public Guid ReceptionDetailId { get; set; }
    /// <summary>
    /// نفش کادر درمان
    /// </summary>
    public MedicalStaff MedicalStaff { get; set; } = new();
    /// <summary>
    /// آیدی نقش کادر درمان
    /// </summary>
    public Guid MedicalStaffId { get; set; }
    /// <summary>
    /// مبلغ سهم
    /// </summary>
    public decimal SharePrice { get; set; }
    /// <summary>
    /// درصد سهم
    /// </summary>
    public float SharePercent { get; set; }

}