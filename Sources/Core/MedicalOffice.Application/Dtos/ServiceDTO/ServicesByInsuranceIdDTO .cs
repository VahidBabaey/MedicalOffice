namespace MedicalOffice.Application.Dtos.ServiceDTO
{
    public class ServicesByInsuranceIdDTO
    {
        public Guid Id { get; set; }
        public string ServiceName { get; set; }
        public decimal TariffValue { get; set; }
    }
}