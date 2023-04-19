using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.Application.Dtos.Tariff
{
    public class CalculateTariffsReqDTO
    {
        public string GenericCode { get; set; }
        public Guid InsuranceId { get; set; }
        public float? Difference { get; set; }
        public bool CalculateDifference { get; set; }
        public int? InsurancePersent { get; set; }
    }
}