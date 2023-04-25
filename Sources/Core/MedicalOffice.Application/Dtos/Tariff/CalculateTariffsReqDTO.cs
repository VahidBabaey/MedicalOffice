using Microsoft.AspNetCore.Mvc;

namespace MedicalOffice.Application.Dtos.Tariff
{
    public class CalculateTariffsReqDTO
    {
        /// <summary>
        /// کد جنریک
        /// </summary>
        public string GenericCode { get; set; }

        /// <summary>
        /// آیدی بیمه
        /// </summary>
        public Guid InsuranceId { get; set; }

        /// <summary>
        /// مابه التفاوت
        /// </summary>
        public float? Difference { get; set; }

        /// <summary>
        /// محاسبه مابه التفاوت
        /// </summary>
        public bool CalculateDifference { get; set; }

        /// <summary>
        /// درصد بیمه
        /// </summary>
        public int? InsurancePersent { get; set; }
    }
}