using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Tariff
{
    public class CalculateTariffsResDTO
    {
        public float InsuranceTariff { get; set; }

        public float Difference { get; set; }

        public float Total { get; set; }
    }
}