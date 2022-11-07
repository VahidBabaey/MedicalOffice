using MedicalOffice.Domain.Common;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// مرکز درمانی - مطب
/// </summary>
public class Office : BaseDomainEntity<Guid>
{
    public Office()
    {
        User = new List<User>();
    }
    /// <summary>
    /// نام
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// آدرس
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// شماره ثابت
    /// </summary>
    public string Tel { get; set; } = string.Empty;

    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین مطب و کاربر-مطب-نقش استفاده می شود
    /// </summary>
    public ICollection<MedicalStaffOfficeRole>? MedicalStaffOfficeRoles { get; set; }

    /// <summary>
    /// نوع تخفیف ها
    /// </summary>
    public ICollection<DiscountType>? DiscountTypes { get; set; }

    /// <summary>
    /// بیمه ها
    /// </summary>
    public ICollection<Insurance>? Insurances { get; set; }

    /// <summary>
    /// ضرایب کا
    /// </summary>
    public ICollection<KMultiplier>? KMultipliers { get; set; }

    /// <summary>
    /// بیماران
    /// </summary>
    public ICollection<Patient>? Patients { get; set; }

    /// <summary>
    /// پذیرش ها
    /// </summary>
    public ICollection<Reception>? Receptions { get; set; }
    /// <summary>
    /// بخش ها
    /// </summary>
    public ICollection<Section>? Sections { get; set; }
    /// <summary>
    /// خدمات
    /// </summary>
    public ICollection<Service>? Services { get; set; }
    /// <summary>
    /// شیفت ها
    /// </summary>
    public ICollection<Shift>? Shifts { get; set; }
    /// <summary>
    /// تعرفه ها
    /// </summary>
    public ICollection<Tariff>? Tariffs { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ICollection<ReceptionDetail>? ReceptionDetails { get; set; }
    /// <summary>
    /// دسترسی ها
    /// </summary>
    public ICollection<Permission>? Permission { get; set; }
    /// <summary>
    /// دسترسی ها
    /// </summary>
    public ICollection<Picture>? Picture { get; set; }

    public Guid UserId{ get; set; }

    /// <summary>
    /// برای ایجاد ارتباط چند به چند بین  کاربر و مطب ها
    /// </summary>
    public ICollection<User> User{ get; set; }
}