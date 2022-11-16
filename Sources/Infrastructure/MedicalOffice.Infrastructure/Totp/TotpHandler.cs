using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalOffice.Application.Contracts.Infrastructure;
using OtpNet;

namespace MedicalOffice.WebApi.Totp
{
    public class TotpHandler : ITotpHandler
    {
        public string Generate(string phoneNamber)
        {
            var bytes = Encoding.Default.GetBytes(phoneNamber);

            var totp = new OtpNet.Totp(bytes, step: 30);

            return totp.ComputeTotp(DateTime.UtcNow);
        }

        public bool Verify(string phoneNumber, string code)
        {
            var totp = new OtpNet.Totp(Encoding.Default.GetBytes(phoneNumber), step: 30);

            var isVerify = totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(1, 1));

            return isVerify;
        }
    }
}
