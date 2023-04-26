using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalOffice.Application.Dtos.Tariff
{
    public class CalculateTariffsResDTO
    {

        /// <summary>
        /// تعرفه بیمه ای
        /// </summary>
        public float InsuranceTariff { get; set; }

        /// <summary>
        /// مابه التفاوت
        /// </summary>
        public float Difference { get; set; }


        /// <summary>
        /// جمع کل
        /// </summary>
        public float Total { get; set; }
    }
}