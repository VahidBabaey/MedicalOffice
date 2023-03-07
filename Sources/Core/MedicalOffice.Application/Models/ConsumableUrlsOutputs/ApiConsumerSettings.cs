using Microsoft.AspNetCore.Http;
using System.Collections;

namespace MedicalOffice.Application.Models.ConsumableUrlsOutputs
{
    public class ApiConsumerSettings
    {
        public string BaseDomain { get; set; }

        public string ServiceGenericCodsPath { get; set; }

        public string AllServicesPath { get; set; }
    }
}