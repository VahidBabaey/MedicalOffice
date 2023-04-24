using MedicalOffice.Application.Dtos.Common.IDtos;

namespace MedicalOffice.Application.Dtos.Common
{
    public class PatientIdDTO : IPatientIdDTO
    {
        public Guid PatientId { get; set; }
    }
}