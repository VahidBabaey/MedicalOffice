using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.Totp
{
    public class TotpSms
    {
        public TotpSms(int type, string[] receptor, string code)
        {
            Type = type;
            Receptor = receptor;
            Code = code;
        }

        public int Type { get; set; } = default;

        public string[] Receptor { get; set; } = { string.Empty };

        public string Code { get; set; } = string.Empty;
    }
}