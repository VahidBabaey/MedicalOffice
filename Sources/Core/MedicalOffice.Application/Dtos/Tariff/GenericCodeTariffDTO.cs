namespace MedicalOffice.Application.Dtos.Tariff
{
    public class ServiceTariffDTO
    {
        public float Private { get; set; }
        public float Govermental { get; set; }
        public float SemiGovermental { get; set; }
        public float Charity { get; set; }
    }
}