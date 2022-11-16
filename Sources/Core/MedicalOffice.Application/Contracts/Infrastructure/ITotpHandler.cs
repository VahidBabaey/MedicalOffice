namespace MedicalOffice.Application.Contracts.Infrastructure
{
    public interface ITotpHandler
    {
        public string Generate(string phoneNamber);
        public bool Verify(string phoneNumber, string code);
    }
}