using MedicalOffice.Application.Dtos.Common;

namespace MedicalOffice.Application.Dtos.InsuranceDTO;

public class InsuranceListIDDTO 
{
    /// <summary>
    /// آی دی بیمه
    /// </summary>
    public Guid[] InsuranceId { get; set; }
}