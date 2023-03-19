﻿using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class UpdateCashPosDTO : BaseDto<Guid>
{
    /// <summary>
    /// آیدی صندوق
    /// </summary>
    public Guid CashId { get; set; }
    /// <summary>
    /// پرداختی
    /// </summary>
    public float Cost { get; set; }
    /// <summary>
    /// آیدی بانک
    /// </summary>
    public Guid BankId { get; set; }
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }

}
