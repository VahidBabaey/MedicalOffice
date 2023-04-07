﻿using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Enums;

namespace MedicalOffice.Application.Dtos.InsuranceDTO;

public class InsuranceDTO
{
    /// <summary>
    /// نام بیمه : تامین - مسلح - غیره
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// کد بیمه
    /// </summary>
    public ulong? InsuranceCode { get; set; }

    /// <summary>
    /// درصد بیمه
    /// </summary>
    public int InsurancePercent { get; set; } = default;

    /// <summary>
    ///  بیمه تکمیلی هست یا نه
    /// </summary>
    public bool IsAdditionalInsurance { get; set; }

    /// <summary>
    /// دارای بیمه تکمیلی هست یا نه
    /// </summary>
    public bool HasAdditionalInsurance { get; set; }

    /// <summary>
    /// نمایش در دیسکت
    /// </summary>
    public bool ShowonDisket { get; set; }

    /// <summary>
    /// عضویت پذیر
    /// </summary>
    public bool Joinable { get; set; }

    /// <summary>
    /// نوع تعرفه
    /// </summary>
    public TariffType TariffType { get; set; }

}