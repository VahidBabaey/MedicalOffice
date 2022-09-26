﻿using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// تخصص
/// </summary>
public class Specialization : BaseDomainEntity<Guid>
{
    /// <summary>
    /// عنوان تخصص
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// تخصص کاربران
    /// </summary>
    public ICollection<UserOfficeSpecialization>? UserOfficeSpecializations { get; set; }
    /// <summary>
    /// بخش ها
    /// </summary>
    public ICollection<Service>? Services { get; set; }
    /// <summary>
    /// پزشکان
    /// </summary>
    public ICollection<MedicalStaff>? Doctors { get; set; }
}