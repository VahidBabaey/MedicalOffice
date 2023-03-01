namespace MedicalOffice.Application.Models.EmailSetting;

public class EmailSettings
{
    public EmailSettings(string apiKey, string fromAddress, string fromName)
    {
        ApiKey = apiKey;
        FromAddress = fromAddress;
        FromName = fromName;
    }

    public string ApiKey { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
}