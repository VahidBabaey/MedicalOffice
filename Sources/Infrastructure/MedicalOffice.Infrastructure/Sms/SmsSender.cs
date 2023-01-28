using Ghasedak.Core;
using MedicalOffice.Application.Contracts.Infrastructure;
using MedicalOffice.Application.Models;
using Microsoft.Extensions.Options;

namespace MedicalOffice.Infrastructure.Sms
{
    public class SmsSender : ISmsSender
    {
        private readonly SmsSettings _smsSettings;
        public SmsSender(IOptions<SmsSettings> smsSettings)
        {
            _smsSettings = smsSettings.Value;
        }

        public async Task<bool> SendTotpSmsAsync(TotpSms totpSms)
        {
            var smsProvider = new Api(_smsSettings.ApiKey);
            var sendMessageToUser = await smsProvider.VerifyAsync(
                totpSms.Type,
                _smsSettings.SendTotpTemplate,
                totpSms.Receptor,
                totpSms.Code);

            if (sendMessageToUser.Items != null)
            {
                return true;
            }
            return false;
        }
    }
}
