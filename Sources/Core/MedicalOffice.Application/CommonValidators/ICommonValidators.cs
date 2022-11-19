namespace MedicalOffice.Application.CommonValidations
{
    public interface ICommonValidators
    {
        Task<bool> ValidPhoneNumber(string phoneNumber);

        Task<bool> validTelePhoneNumber(string telePhoneNumber);

        Task<bool> ValidNationalId(string NationalId);
    }
}
