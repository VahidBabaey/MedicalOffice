namespace MedicalOffice.Application.Dtos.ServiceDTO
{
    public class ServicesByInsuranceIdDTO
    {
        public Guid Id { get; set; }
        
        public string ServiceName { get; set; }

        /// <summary>
        /// تعرفه بر اساس کد جنریک
        /// </summary>
        public float TariffValue { get; set; }
        
        /// <summary>
        /// تعیید تعرفه در زمان پذیرش
        /// </summary>
        public bool TariffInReceptionTime { get; set; }

        /// <summary>
        /// زمان خدمت
        /// </summary>
        public uint? ServiceTime { get; set; } = 0;
    }
}