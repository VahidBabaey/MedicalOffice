﻿using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.MembershipDTO;

public class UpdateMembershipDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// نام عضویت
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool IsActive { get; set; }
    /// <summary>
    /// تخفیف
    /// </summary>
    public string Discount { get; set; } = string.Empty;


}
