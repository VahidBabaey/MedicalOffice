namespace MedicalOffice.Application.Models.ApiConsumerModels
{
    public class ServiceTariffModel
    {
        public float Private { get; set; }
        public float Govermental { get; set; }
        public float SemiGovermental { get; set; }
        public float Charity { get; set; }
    }
}