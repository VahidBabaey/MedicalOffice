﻿using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.InsuranceDTO;

public class InsuranceListDTO : BaseDto<Guid>
{

    /// <summary>
    /// نام بیمه : تامین - مسلح - غیره
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// کد بیمه
    /// </summary>
    public long InsuranceCode { get; set; }
    /// <summary>
    /// درصد بیمه
    /// </summary>
    public float InsurancePercent { get; set; }
    /// <summary>
    ///  بیمه تکمیلی هست یا نه
    /// </summary>
    public bool IsAdditionalInsurance { get; set; }
    /// <summary>
    /// دارای بیمه تکمیلی هست یا نه
    /// </summary>
    public bool HasAdditionalInsurance { get; set; }

    /// <summary>
    /// عضویت پذیر
    /// </summary>
    public bool Joinable { get; set; }
}