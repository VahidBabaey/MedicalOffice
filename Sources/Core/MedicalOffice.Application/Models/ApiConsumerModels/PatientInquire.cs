using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Models.ApiConsumerModels
{
    public class PatientInquire
    {
        public string SalamatUsername { get; set; }
        public string SalamatPassword { get; set; }
        public string DoctorMedicalId { get; set; }
        public string NationalCode { get; set; }
    }
}
