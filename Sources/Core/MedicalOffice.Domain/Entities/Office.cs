using MedicalOffice.Domain.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Domain.Entities;

/// <summary>
/// مرکز درمانی - مطب
/// </summary>
public class Office : BaseDomainEntity<Guid>
{
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
    public string TelePhoneNumber { get; set; } = string.Empty;
    /// <summary>
    /// نوع تعرفه
    /// </summary>
    public TariffType? TariffType { get; set; }
    /// <summary>
    /// نوع تخفیف ها
    /// </summary>
    public ICollection<DiscountType> DiscountTypes { get; set; }
    /// <summary>
    /// بیمه ها
    /// </summary>
    public ICollection<Insurance> Insurances { get; set; }

    /// <summary>
    /// ضرایب کا
    /// </summary>
    public ICollection<KMultiplier> KMultipliers { get; set; }
    /// <summary>
    /// بیماران
    /// </summary>
    public ICollection<Patient> Patients { get; set; }
    /// <summary>
    /// پذیرش ها
    /// </summary>
    public ICollection<Reception> Receptions { get; set; }
    /// <summary>
    /// بخش ها
    /// </summary>
    public ICollection<Section> Sections { get; set; }
    /// <summary>
    /// خدمات
    /// </summary>
    public ICollection<Service> Services { get; set; }
    /// <summary>
    /// شیفت ها
    /// </summary>
    public ICollection<Shift> Shifts { get; set; }
    /// <summary>
    /// تعرفه ها
    /// </summary>
    public ICollection<Tariff> Tariffs { get; set; }
    /// <summary>
    /// جزئیات پذیرش
    /// </summary>
    public ICollection<ReceptionDetail> ReceptionDetails { get; set; }
    /// <summary>
    /// دسترسی ها
    /// </summary>
    public ICollection<Picture> Pictures { get; set; }
    /// <summary>
    /// ارتباط یک به چند مطب با کاربرانش
    /// </summary>
    public ICollection<MedicalStaff> MedicalStaffs { get; set; } = new HashSet<MedicalStaff>();
    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین مطب و کاربر-مطب-نقش استفاده می شود
    /// </summary>
    public ICollection<UserOfficeRole> UserOfficeRoles { get; set; }
    /// <summary>
    /// از این مدل برای برقراری ارتباط یک به چند بین مطب و دسترسی های کاربر مطب استفاده می شود
    /// </summary>
    public ICollection<UserOfficePermission> UserOfficePermissions { get; set; }
    /// <summary>
    /// وقت دهی
    /// </summary>
    public ICollection<Appointment> Appointments{ get; set; }
    /// <summary>
    /// کارت
    /// </summary>
    public ICollection<CashCart>? CashCarts { get; set; }
    /// <summary>
    /// پوز
    /// </summary>
    public ICollection<CashPos>? CashPoses { get; set; }
    /// <summary>
    /// چک
    /// </summary>
    public ICollection<CashCheck>? CashChecks { get; set; }
    /// <summary>
    /// معرفان
    /// </summary>
    public ICollection<Introducer>? Introducers { get; set; }
    /// <summary>
    /// عضویت ها
    /// </summary>
    public ICollection<Membership>? Memberships { get; set; }
    /// <summary>
    /// سرویس ها - عضویت ها
    /// </summary>
    public ICollection<MemberShipService>? MemberShipServices { get; set; }
    /// <summary>
    /// جزئیات
    /// </summary>
    public ICollection<BasicInfo>? BasicInfos { get; set; }
    /// <summary>
    /// فرم استعلاجی
    /// </summary>
    public ICollection<FormIllness>? FormIllnesses { get; set; }
    /// <summary>
    /// فرم ارجاع
    /// </summary>
    public ICollection<FormReferal>? FormReferals { get; set; }
    /// <summary>
    /// فرم تعهد
    /// </summary>
    public ICollection<FormCommitment>? FormCommitments { get; set; }
    public ICollection<ServiceDuration> ServiceDuration { get; set; }
}