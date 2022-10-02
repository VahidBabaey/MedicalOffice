﻿using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.Section;

public class UpdateSectionDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی مطب
    /// </summary>
    public Guid OfficeId { get; set; }
    /// <summary>
    /// نام بخش
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// فعال یا غیرفعال
    /// </summary>
    public bool IsActive { get; set; }


}
