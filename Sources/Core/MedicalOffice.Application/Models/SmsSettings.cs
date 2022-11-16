using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models
{
    public class SmsSettings
    {
        public SmsSettings()
        {

        }
        public SmsSettings(string apiKey, string sendTotpTemplate)
        {
            ApiKey = apiKey;
            SendTotpTemplate = sendTotpTemplate;
        }
        public string? ApiKey{ get; set; } 
        public string? SendTotpTemplate { get; set; }
    }
}
