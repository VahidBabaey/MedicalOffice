using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.Application.Dtos.ReceptionDTO
{
    public class CalculateDiscountDTO
    {
        public Guid ServiceId { get; set; }

        public int ServiceCount { get; set; }

        public Guid InsuranceId { get; set; }

        public Guid? AdditionalInsuranceId { get; set; }

        public int? Discount { get; set; }

        public long Tariff { get; set; }
    }
}