using MedicalOffice.Application.Dtos.AppointmentsDTO.Commons;
using MedicalOffice.Application.Dtos.Common;
using MedicalOffice.Domain.Entities;

namespace MedicalOffice.Application.Dtos.CashDTO;

public class CashesDTO : IReceptionIdDTO
{
    /// <summary>
    /// آیدی پذیرش
    /// </summary>
    public Guid ReceptionId { get; set; }
    /// <summary>
    /// دریافتی
    /// </summary>
    public long Recieved { get; set; }

}
