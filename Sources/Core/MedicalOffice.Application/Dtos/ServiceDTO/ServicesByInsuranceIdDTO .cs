namespace MedicalOffice.Application.Dtos.ServiceDTO
{
    public class ServicesByInsuranceIdDTO
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        // تعرفه سرویس
        public float TariffValue { get; set; }
        public bool TariffInReceptionTime { get; set; }
    }
}