using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// خدمت وقت دهی
/// </summary>
public class AppointmentService : BaseDomainEntity<Guid>
{
    /// <summary>
    /// وقت دهی
    /// </summary>
    public Appointment? Appointment { get; set; }
    /// <summary>
    /// آیدی وقت دهی
    /// </summary>
    public Guid AppointmentId { get; set; }
    /// <summary>
    /// خدمت
    /// </summary>
    public Service? Service { get; set; }
    /// <summary>
    /// آیدی خدمت
    /// </summary>
    public Guid ServiceId { get; set; }
}