using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalOffice.Application.Contracts.Infrastructure;

namespace MedicalOffice.WebApi.Totp
{
    public class TotpHandler : ITotpHandler
    {
        public string Generate(string phoneNamber)
        {
            var bytes = Encoding.Default.GetBytes(phoneNamber);

            var totp = new OtpNet.Totp(bytes, step: 30 * 60 * 1000);

            return totp.ComputeTotp(DateTime.UtcNow);
        }

        public bool Verify(string phoneNumber, string code)
        {
            var totp = new OtpNet.Totp(Encoding.Default.GetBytes(phoneNumber), step: 30 * 60 * 1000);

            long unixTimestamp = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;

            var isVerify = totp.VerifyTotp(code, out unixTimestamp);

            return isVerify;
        }
    }
}
