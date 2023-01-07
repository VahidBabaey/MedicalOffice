using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// نوع وقت دهی
/// </summary>
public class AppointmentTypePast : BaseDomainEntity<Guid>
{
    /// <summary>
    /// عنوان نوع وقت دهی
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// وقت دهی ها
    /// </summary>
    //public ICollection<Appointment>? Appointments { get; set; }
}