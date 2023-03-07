using Microsoft.AspNetCore.Http;
using System.Collections;

namespace MedicalOffice.Application.Models.ApiConsumerModels
{
    public class ApiConsumerSettings
    {
        public string BaseDomain { get; set; }

        public string ServiceGenericCodsPath { get; set; }

        public string AllServicesPath { get; set; }

        public string ServiceTariffsPath { get; set; }
    }
}